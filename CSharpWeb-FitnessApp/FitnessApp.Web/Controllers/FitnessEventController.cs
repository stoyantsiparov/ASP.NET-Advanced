using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
    public class FitnessEventController : BaseController
    {
        private readonly IFitnessEventService _fitnessEventService;

        public FitnessEventController(IFitnessEventService fitnessEventService)
        {
            _fitnessEventService = fitnessEventService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _fitnessEventService.GetAllFitnessEventsAsync();

            return View(model);
        }

        public async Task<IActionResult> MyFitnessEvents()
        {
            var userId = GetUserId();

            var model = await _fitnessEventService.GetMyFitnessEventsAsync(userId);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _fitnessEventService.GetFitnessEventDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToMyFitnessEvents(int id)
        {
            var userId = GetUserId();  

            try
            {
                var model = await _fitnessEventService.GetFitnessEventByIdAsync(id);
                if (model == null)
                {
                    ModelState.AddModelError(string.Empty, "The fitness event does not exist.");
                    return RedirectToAction(nameof(Details), new { id });
                }

                await _fitnessEventService.AddToMyFitnessEventsAsync(userId, model, DateTime.Now);

                return RedirectToAction(nameof(MyFitnessEvents));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        public async Task<IActionResult> RemoveFromMyFitnessEvents(int id)
        {
            var userId = GetUserId();

            try
            {
                var model = await _fitnessEventService.GetFitnessEventByIdAsync(id);
                await _fitnessEventService.RemoveFromMyFitnessEventsAsync(userId, model);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(MyFitnessEvents));
        }
    }
}