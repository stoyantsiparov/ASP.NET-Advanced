using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		ViewData["Title"] = "Achieve Your Fitness Goals";
		ViewData["Message"] = "Join our fitness and wellness community today!";
		ViewData["CTAButtonText"] = "Get Started";
		ViewData["Description"] = "Explore our fitness classes and rejuvenating spa services. Whether you’re looking to get fit or relax, we’ve got you covered.";
		return View();
	}

	public IActionResult Error(int? statusCode = null)
	{
		if (statusCode == 404)
		{
			return View("Error404");
		}

		return View("Error500");
	}
}