using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.SpaProcedure;
using static FitnessApp.Common.SuccessfulValidationMessages.SpaProcedure;

namespace FitnessApp.Web.Controllers;

public class SpaProcedureController : BaseController
{
	private readonly ISpaProcedureService _spaService;

	public SpaProcedureController(ISpaProcedureService spaService)
	{
		_spaService = spaService;
	}

    [AllowAnonymous]
    public async Task<IActionResult> Index(string? searchQuery = null, int pageNumber = 1, int pageSize = 4)
    {
        var model = await _spaService.GetAllSpaProceduresPaginationAsync(searchQuery, pageNumber, pageSize);

        return View(model);
    }

    public async Task<IActionResult> MySpaAppointments()
	{
		var userId = GetUserId();

		var model = await _spaService.GetMySpaProceduresAsync(userId);

		return View(model);
	}

	[AllowAnonymous]
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
            TempData["SuccessMessage"] = SpaProcedureAddedSuccessfully;
        }
		catch (InvalidOperationException ex)
		{
			TempData["ErrorMessage"] = ex.Message;
			return RedirectToAction(nameof(Details));
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
}