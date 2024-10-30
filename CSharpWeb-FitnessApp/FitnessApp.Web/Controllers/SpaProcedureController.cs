using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
	public class SpaProcedureController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
