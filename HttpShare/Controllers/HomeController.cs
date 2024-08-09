using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;


[Controller]
[Route("/")]
public sealed class HomeController : Controller
{
	[HttpGet]
	[Route("/")]
	public IActionResult Index()
	{
		ViewData["PageTitle"] = "HTTP Share";
		return View();
	}
}
