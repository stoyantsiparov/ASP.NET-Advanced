using FitnessApp.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FitnessApp.Web.Controllers;

public class InstructorController : BaseController
{
    private readonly IInstructorService _instructorService;

    public InstructorController(IInstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    [AllowAnonymous]
	public async Task<IActionResult> Index(string? searchQuery = null)
	{
		var model = await _instructorService.GetAllInstructorsAsync(searchQuery);

		return View(model);
	}

	[AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var model = await _instructorService.GetInstructorDetailsAsync(id);

        if (model == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}