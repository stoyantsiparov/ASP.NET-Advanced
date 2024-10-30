using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.Web.ViewModels;

namespace FitnessApp.Web.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{

		}

		public IActionResult Index()
		{
			ViewData["Title"] = "Home Page";
			ViewData["Message"] = "Welcome to the Fitness Web App!";

			return View();
		}
	}
}
