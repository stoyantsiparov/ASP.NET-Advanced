using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;

namespace FitnessApp.Web.Areas.Admin.Controllers;

[Area(AdminRole)]
[Authorize(Roles = AdminRole)]
public class SpaProceduresManagementController : BaseController
{
	private readonly ISpaProcedureService _spaService;

	public SpaProceduresManagementController(ISpaProcedureService spaService)
	{
		_spaService = spaService;
	}

	public async Task<IActionResult> Index()
	{
		var spaProcedures = await _spaService.GetAllSpaProceduresAsync();

		return View(spaProcedures);
	}

	[HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = await _spaService.GetSpaProcedureForAddAsync();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddSpaProcedureViewModel model)
	{
		if (ModelState.IsValid == false)
		{
			return View(model);
		}

		var userId = GetUserId();
		await _spaService.AddSpaProcedureAsync(model, userId);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var model = await _spaService.GetSpaProceduresByIdAsync(id);

		if (model != null)
		{
			return View(model);
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<IActionResult> Edit(SpaProceduresViewModel model)
	{
		if (ModelState.IsValid == false)
		{
			return View(model);
		}

		var userId = GetUserId();

		await _spaService.EditSpaProcedureAsync(model, userId);

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Delete(int id)
	{
		var model = await _spaService.GetSpaProcedureForDeleteAsync(id);

		if (model != null)
		{
			return View(model);
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<IActionResult> Delete(DeleteSpaProcedureViewModel model)
	{
		var userId = GetUserId();

		await _spaService.DeleteSpaProcedureAsync(model.Id, userId);

		return RedirectToAction(nameof(Index));
	}
}