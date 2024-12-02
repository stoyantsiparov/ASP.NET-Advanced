using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.SpaProcedureViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.SpaProcedure;
using static FitnessApp.Common.ValidationMessages.SpaProcedure;

namespace FitnessApp.Web.Controllers;

public class SpaProcedureController : BaseController
{
	private readonly ISpaProcedureService _spaService;

	public SpaProcedureController(ISpaProcedureService spaService)
	{
		_spaService = spaService;
	}

	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		var model = await _spaService.GetAllSpaProceduresAsync();

		return View(model);
	}

	public async Task<IActionResult> MySpaAppointments()
	{
		var userId = GetUserId();

		var model = await _spaService.GetMySpaProceduresAsync(userId);

		return View(model);
	}

	public async Task<IActionResult> Details(int id)
	{
		var model = await _spaService.GetSpaProceduresDetailsAsync(id);

		if (model == null)
		{
			TempData["ErrorMessage"] = SpaAppointmentNotBooked;
			return RedirectToAction(nameof(Index));
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> AddToMySpaAppointments(int id, DateTime appointmentDateTime)
	{
		var model = await _spaService.GetSpaProceduresByIdAsync(id);

		if (model == null)
		{
			TempData["ErrorMessage"] = SpaAppointmentNotBooked;
			return RedirectToAction(nameof(MySpaAppointments));
		}

		var userId = GetUserId();

		try
		{
			await _spaService.AddToMySpaAppointmentsAsync(userId, model, appointmentDateTime);
			TempData["SuccessMessage"] = "Spa appointment added successfully.";
		}
		catch (InvalidOperationException ex)
		{
			TempData["ErrorMessage"] = ex.Message;
			return RedirectToAction(nameof(Details), new { id });
		}

		return RedirectToAction(nameof(MySpaAppointments));
	}

	public async Task<IActionResult> RemoveFromMySpaAppointment(int id)
	{
		var model = await _spaService.GetSpaProceduresByIdAsync(id);

		if (model == null)
		{
			TempData["ErrorMessage"] = SpaAppointmentNotBooked;
			return RedirectToAction(nameof(MySpaAppointments));
		}

		var userId = GetUserId();

		try
		{
			await _spaService.RemoveFromMySpaAppointmentsAsync(userId, model);
			TempData["SuccessMessage"] = SpaAppointmentRemovedSuccessfully;
		}
		catch (InvalidOperationException ex)
		{
			TempData["ErrorMessage"] = ex.Message;
			return RedirectToAction(nameof(MySpaAppointments));
		}

		return RedirectToAction(nameof(MySpaAppointments));
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

		await _spaService.EditSpaProcedureAsync(model);

		return RedirectToAction(nameof(Details), new { id = model.Id });
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
		await _spaService.DeleteSpaProcedureAsync(model.Id);

		return RedirectToAction(nameof(Index));
	}
}