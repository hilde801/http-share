using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;


[Controller]
[Route("/")]
public sealed class HomeController(ServerSession serverSession) : Controller
{
	[HttpGet]
	[Route("/")]
	public IActionResult Index()
	{
		ViewData["PageTitle"] = $"{serverSession.HostName} - HTTP Share";

		bool isInSendMode = serverSession is ISendSession,
			isInReceiveMode = serverSession is IReceiveSession;

		return Ok(new { Message = "Will be implemented later!" });
	}
}
