using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
	public class ClassController : BaseController
	{
		private readonly IClassService _classService;

		public ClassController(IClassService classService)
		{
			_classService = classService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var model = await _classService.GetAllClassesAsync();

			return View(model);
		}

		public async Task<IActionResult> MyClasses()
		{
			var userId = GetUserId();

			var model = await _classService.GetMyClassesAsync(userId);

			return View(model);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var model = await _classService.GetClassDetailsAsync(id);

			if (model == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddToMyClasses(int id, DateTime appointmentDateTime)
		{
			if (appointmentDateTime < DateTime.Now)
			{
				ModelState.AddModelError(string.Empty, "Appointment date and time cannot be in the past.");
				return RedirectToAction(nameof(Details), new { id });
			}

			var userId = GetUserId();

			try
			{
				var model = await _classService.GetClassByIdAsync(id);
				await _classService.AddToMyClassesAsync(userId, model, appointmentDateTime);
			}
			catch (InvalidOperationException ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return RedirectToAction(nameof(Details), new { id });
			}

			return RedirectToAction(nameof(MyClasses));
		}

		public async Task<IActionResult> RemoveFromMyClasses(int id)
		{
			var userId = GetUserId();

			try
			{
				var model = await _classService.GetClassByIdAsync(id);
				await _classService.RemoveFromMyClassesAsync(userId, model);
			}
			catch (InvalidOperationException ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			return RedirectToAction(nameof(MyClasses));
		}
	}
}