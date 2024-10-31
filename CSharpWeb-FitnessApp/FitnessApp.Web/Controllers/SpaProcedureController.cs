using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
    public class SpaProcedureController : BaseController
    {
        private readonly ISpaProcedureService _spaProcedureService;

        public SpaProcedureController(ISpaProcedureService spaProcedureService)
        {
            _spaProcedureService = spaProcedureService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _spaProcedureService.GetAllSpaProceduresAsync();

            return View(model);
        }

        public async Task<IActionResult> MySpaAppointments()
        {
            var userId = GetUserId();
            var model = await _spaProcedureService.GetMySpaProceduresAsync(userId);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _spaProcedureService.GetSpaProceduresDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> AddToMySpaAppointments(int id)
        {
            var model = await _spaProcedureService.GetSpaProceduresByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(MySpaAppointments));
            }

            var userId = GetUserId();

            var userAppointments = await _spaProcedureService.GetMySpaProceduresAsync(userId);

            if (userAppointments.Any(a => a.Id == id))
            {
                return RedirectToAction(nameof(Index));
            }

            await _spaProcedureService.AddToMySpaAppointmentsAsync(userId, model);

            return RedirectToAction(nameof(MySpaAppointments));
        }

        public async Task<IActionResult> RemoveFromToMySpaAppointment(int id)
        {
            var model = await _spaProcedureService.GetSpaProceduresByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(MySpaAppointments));
            }

            var userId = GetUserId();
            await _spaProcedureService.RemoveFromMySpaAppointmentsAsync(userId, model);

            return RedirectToAction(nameof(Index));
        }
    }
}
