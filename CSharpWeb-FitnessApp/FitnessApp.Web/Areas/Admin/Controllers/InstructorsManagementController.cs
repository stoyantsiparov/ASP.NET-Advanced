using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.InstructorViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.ErrorMessages.Instructor;
using static FitnessApp.Common.SuccessfulValidationMessages.Instructor;

namespace FitnessApp.Web.Areas.Admin.Controllers;

[Area(AdminRole)]
[Authorize(Roles = AdminRole)]
public class InstructorsManagementController : BaseController
{
    private readonly IInstructorService _instructorService;

    public InstructorsManagementController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    public async Task<IActionResult> Index()
    {
        var instructors = await _instructorService.GetAllInstructorsAsync();

        return View(instructors);
    }

    [HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = await _instructorService.GetInstructorForAddAsync();

		return View(model);
	}

	[HttpPost]
    [ValidateAntiForgeryToken]
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
    [ValidateAntiForgeryToken]
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
			return RedirectToAction(nameof(Index));
		}
		catch (Exception)
		{
			ModelState.AddModelError(string.Empty, InstructorEditError);
			return View(model);
		}
	}

	[HttpGet]
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
    [ValidateAntiForgeryToken]
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