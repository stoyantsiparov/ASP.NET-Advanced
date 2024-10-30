using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Web.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
		protected string GetUserId()
		{
			string id = string.Empty;

			if (User != null)
			{
				id = User.FindFirstValue(ClaimTypes.NameIdentifier);
			}
			return id;
		}
	}
}
