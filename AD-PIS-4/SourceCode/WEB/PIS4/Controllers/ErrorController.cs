using Microsoft.AspNetCore.Mvc;

namespace ColorAdmin.Controllers
{
    public class ErrorController : Controller
		{
				[Route("/Error/{statusCode}")]
				public IActionResult HttpStatusCodeHandler(int statusCode)
				{
						if (statusCode == 404)
						{
								return View("404");
						}

						return View("Error");
				}
		}
}
