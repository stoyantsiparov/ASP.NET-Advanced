using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.ErrorMessages.Instructor;
using static FitnessApp.Common.SuccessfulValidationMessages.Instructor;


namespace FitnessApp.Web.Controllers;

public class InstructorController : BaseController
{
    private readonly IInstructorService _instructorService;

    public InstructorController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = await _instructorService.GetAllInstructorsAsync();

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var model = await _instructorService.GetInstructorDetailsAsync(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Add()
    {
        var model = await _instructorService.GetInstructorForAddAsync();

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Add(AddInstructorViewModel model)
    {
        if (model == null)
        {
            ModelState.AddModelError(string.Empty, InstructorViewModelCannotBeNull);
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError(string.Empty, UserIdCannotBeEmpty);
                return View(model);
            }

            await _instructorService.AddInstructorAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, InstructorAddError);
            return View(model);
        }
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var model = await _instructorService.GetInstructorByIdAsync(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Edit(InstructorViewModel model)
    {
        if (model == null)
        {
            ModelState.AddModelError(string.Empty, InstructorViewModelCannotBeNull);
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = GetUserId();

        try
        {
            await _instructorService.EditInstructorAsync(model, userId);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, InstructorEditError);
            return View(model);
        }
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var model = await _instructorService.GetInstructorForDeleteAsync(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Delete(DeleteInstructorViewModel model)
    {
        if (model == null || model.Id <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var userId = GetUserId();

        try
        {
            await _instructorService.DeleteInstructorAsync(model.Id, userId);
            TempData["SuccessMessage"] = InstructorDeletedSuccessfully;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = InstructorDeleteError;
        }

        return RedirectToAction(nameof(Index));
    }
}