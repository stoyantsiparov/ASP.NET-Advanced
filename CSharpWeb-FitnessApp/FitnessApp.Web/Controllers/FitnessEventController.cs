using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.ViewModels.FitnessEventViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ErrorMessages.FitnessEvent;

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

        public async Task<IActionResult> Details(int id)
        {
            var model = await _fitnessEventService.GetFitnessEventDetailsAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> AddToMyFitnessEvents(int id)
        {
            var userId = GetUserId();

            var model = await _fitnessEventService.GetFitnessEventByIdAsync(id);
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, FitnessEventDoesNotExist);
                return RedirectToAction(nameof(Details), new { id });
            }

            try
            {
                await _fitnessEventService.AddToMyFitnessEventsAsync(userId, model);
                return RedirectToAction(nameof(MyFitnessEvents));
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
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

            await _fitnessEventService.EditFitnessEventAsync(model);

            return RedirectToAction(nameof(Details), new { id = model.Id });
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
            await _fitnessEventService.DeleteFitnessEventAsync(model.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}