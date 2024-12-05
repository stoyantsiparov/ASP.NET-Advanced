using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.ClassViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.Class;
using static FitnessApp.Common.ApplicationsConstants;

namespace FitnessApp.Web.Controllers;

public class ClassController : BaseController
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = await _classService.GetAllClassesAsync();

        return View(model);
    }

    public async Task<IActionResult> MyClasses()
    {
        var userId = GetUserId();

        var model = await _classService.GetMyClassesAsync(userId);

        return View(model);
    }

    [AllowAnonymous]
	public async Task<IActionResult> Details(int id)
    {
        var model = await _classService.GetClassDetailsAsync(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<IActionResult> AddToMyClasses(int id)
    {
        var userId = GetUserId();

        try
        {
            var model = await _classService.GetClassByIdAsync(id);

            if (model == null)
            {
                ModelState.AddModelError(string.Empty, FitnessClassDoesNotExist);
                return RedirectToAction(nameof(Details), new { id });
            }

            await _classService.AddToMyClassesAsync(userId, model);

            return RedirectToAction(nameof(MyClasses));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }

    public async Task<IActionResult> RemoveFromMyClasses(int id)
    {
        var userId = GetUserId();

        try
        {
            var model = await _classService.GetClassByIdAsync(id);
            await _classService.RemoveFromMyClassesAsync(userId, model);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return RedirectToAction(nameof(MyClasses));
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Add()
    {
        var model = await _classService.GetClassForAddAsync();

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Add(AddClassViewModel model)
    {
        if (ModelState.IsValid == false)
        {
            var instructors = await _classService.GetClassForAddAsync();
            model.Instructors = instructors.Instructors;
            return View(model);
        }

        var userId = GetUserId();
        await _classService.AddClassAsync(model, userId);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _classService.GetClassByIdAsync(id);

        if (model != null)
        {
            var instructors = await _classService.GetClassForAddAsync();
            model.Instructors = instructors.Instructors;
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Edit(ClassesViewModel model)
    {
        if (ModelState.IsValid == false)
        {
            var instructors = await _classService.GetClassForAddAsync();
            model.Instructors = instructors.Instructors;
            return View(model);
        }

        var userId = GetUserId();

        await _classService.EditClassAsync(model, userId);

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await _classService.GetClassForDeleteAsync(id);

        if (model != null)
        {
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Delete(DeleteClassViewModel model)
    {
        var userId = GetUserId();

        await _classService.DeleteClassAsync(model.Id, userId);

        return RedirectToAction(nameof(Index));
    }
}