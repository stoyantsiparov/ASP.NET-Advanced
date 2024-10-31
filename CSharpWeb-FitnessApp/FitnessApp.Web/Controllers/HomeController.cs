using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FitnessApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FitnessApp.Web.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController()
		{

		}

		[AllowAnonymous]
		public IActionResult Index()
		{
			ViewData["Title"] = "Home Page";
			ViewData["Message"] = "Welcome to the Fitness Web App!";

			return View();
		}
	}
}
