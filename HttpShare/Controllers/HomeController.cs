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

		bool sendSession = serverSession is ISendSession,
			receiveSession = serverSession is IReceiveSession;

		ViewData["SendSession"] = sendSession;
		ViewData["ReceiveSession"] = receiveSession;

		if (sendSession) ViewData["OutboxFiles"] = (serverSession as ISendSession)!.OutboxFiles;

		return View();
	}
}
