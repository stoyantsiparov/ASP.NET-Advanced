using FitnessApp.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FitnessApp.Common.ApplicationsConstants;

namespace FitnessApp.Web.Areas.Admin.Controllers;

[Area(AdminRole)]
[Authorize(Roles = AdminRole)]
public class HomeController : BaseController
{
	public IActionResult Index()
	{
		return View();
	}
}