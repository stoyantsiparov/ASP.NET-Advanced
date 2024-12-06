using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.FitnessEventViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;

namespace FitnessApp.Web.Areas.Admin.Controllers;

[Area(AdminRole)]
[Authorize(Roles = AdminRole)]
public class FitnessEventsManagementController : BaseController
{
	private readonly IFitnessEventService _fitnessEventService;

	public FitnessEventsManagementController(IFitnessEventService fitnessEventService)
	{
		_fitnessEventService = fitnessEventService;
	}

	public async Task<IActionResult> Index()
	{
		var fitnessEvents = await _fitnessEventService.GetAllFitnessEventsAsync();

		return View(fitnessEvents);
	}

	[HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = await _fitnessEventService.GetFitnessEventForAddAsync();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddFitnessEventViewModel model)
	{
		if (ModelState.IsValid == false)
		{
			return View(model);
		}

		var userId = GetUserId();
		await _fitnessEventService.AddFitnessEventAsync(model, userId);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var model = await _fitnessEventService.GetFitnessEventByIdAsync(id);

		if (model != null)
		{
			return View(model);
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<IActionResult> Edit(FitnessEventViewModel model)
	{
		if (ModelState.IsValid == false)
		{
			return View(model);
		}

		var userId = GetUserId();

		await _fitnessEventService.EditFitnessEventAsync(model, userId);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Delete(int id)
	{
		var model = await _fitnessEventService.GetFitnessEventForDeleteAsync(id);

		if (model != null)
		{
			return View(model);
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<IActionResult> Delete(DeleteFitnessEventViewModel model)
	{
		var userId = GetUserId();

		await _fitnessEventService.DeleteFitnessEventAsync(model.Id, userId);

		return RedirectToAction(nameof(Index));
	}
}