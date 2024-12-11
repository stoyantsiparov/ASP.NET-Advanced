using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.Class;
using static FitnessApp.Common.SuccessfulValidationMessages.Class;

namespace FitnessApp.Web.Controllers;

public class ClassController : BaseController
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(string? searchQuery = null, int? minDuration = null, int? maxDuration = null)
    {
        var model = await _classService
            .GetAllClassesAsync(searchQuery, minDuration, maxDuration);

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
            TempData["ErrorMessage"] = UserNotRegisteredForClass;
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    public async Task<IActionResult> AddToMyClasses(int id)
    {
        var model = await _classService.GetClassByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = FitnessClassDoesNotExist;
            return RedirectToAction(nameof(Details), new { id });
        }

        var userId = GetUserId();

        try
        {
            await _classService.AddToMyClassesAsync(userId, model);
            TempData["SuccessMessage"] = ClassAddedSuccessfully;
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Details), new { id });
        }

        return RedirectToAction(nameof(MyClasses));
    }

    public async Task<IActionResult> RemoveFromMyClasses(int id)
    {
        var model = await _classService.GetClassByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = UserNotRegisteredForClass;
            return RedirectToAction(nameof(MyClasses));
        }

        var userId = GetUserId();

        try
        {
            await _classService.RemoveFromMyClassesAsync(userId, model);
            TempData["SuccessMessage"] = ClassAppointmentRemovedSuccessfully;
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(MyClasses));
        }

        return RedirectToAction(nameof(MyClasses));
    }
}