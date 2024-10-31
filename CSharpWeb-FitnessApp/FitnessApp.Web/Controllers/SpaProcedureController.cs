using FitnessApp.Services.Data;
using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
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

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _spaService.GetSpaProceduresDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            model.TreatmentDaysOptions = await _spaService.GetTreatmentDaysAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMySpaAppointments(int id, string treatmentDay)
        {
            var model = await _spaService.GetSpaProceduresByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(MySpaAppointments));
            }

            var userId = GetUserId();

            var userAppointments = await _spaService.GetMySpaProceduresAsync(userId);

            if (userAppointments.Any(a => a.Id == id))
            {
                // Optional: Add a message to the user
                return RedirectToAction(nameof(Index));
            }

            await _spaService.AddToMySpaAppointmentsAsync(userId, model, treatmentDay);

            return RedirectToAction(nameof(MySpaAppointments));
        }


        public async Task<IActionResult> RemoveFromToMySpaAppointment(int id)
        {
            var model = await _spaService.GetSpaProceduresByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(MySpaAppointments));
            }

            var userId = GetUserId(); 

            await _spaService.RemoveFromMySpaAppointmentsAsync(userId, model);

            return RedirectToAction(nameof(MySpaAppointments));
        }
    }
}
