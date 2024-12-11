using FitnessApp.Services.Data.Contracts;
using FitnessApp.Web.Controllers;
using FitnessApp.Web.ViewModels.ClassViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;
using static FitnessApp.Common.SuccessfulValidationMessages.Class;
using static FitnessApp.Common.ErrorMessages.Class;

namespace FitnessApp.Web.Areas.Admin.Controllers
{
    [Area(AdminRole)]
    [Authorize(Roles = AdminRole)]
    public class ClassesManagementController : BaseController
    {
        private readonly IClassService _classService;

        public ClassesManagementController(IClassService classService)
        {
            _classService = classService;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _classService.GetAllClassesAsync();

            return View(classes);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await _classService.GetClassForAddAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = InvalidData;
                var instructors = await _classService.GetClassForAddAsync();
                model.Instructors = instructors.Instructors;
                return View(model);
            }

            var userId = GetUserId();
            await _classService.AddClassAsync(model, userId);

            TempData["SuccessMessage"] = ClassAddedSuccessfully;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _classService.GetClassByIdAsync(id);

            if (model != null)
            {
                var instructors = await _classService.GetClassForAddAsync();
                model.Instructors = instructors.Instructors;
                return View(model);
            }

            TempData["ErrorMessage"] = ClassNotFound;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClassesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = InvalidData;
                var instructors = await _classService.GetClassForAddAsync();
                model.Instructors = instructors.Instructors;
                return View(model);
            }

            var userId = GetUserId();

            await _classService.EditClassAsync(model, userId);

            TempData["SuccessMessage"] = ClassUpdatedSuccessfully;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _classService.GetClassForDeleteAsync(id);

            if (model != null)
            {
                return View(model);
            }

            TempData["ErrorMessage"] = ClassNotFound;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteClassViewModel model)
        {
            var userId = GetUserId();

            await _classService.DeleteClassAsync(model.Id, userId);

            TempData["SuccessMessage"] = ClassDeletedSuccessfully;
            return RedirectToAction(nameof(Index));
        }
    }
}