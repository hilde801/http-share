// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using HttpShare.Sessions;

using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

/// <summary>
/// The default controller class.
/// </summary>
/// <param name="serverSession">The selected <see cref="ServerSession"/> object.</param>
[Controller]
[Route("/")]
public sealed class HomeController(ServerSession serverSession) : CustomController(serverSession)
{
	/// <summary>
	/// Handles requests to address /.
	/// </summary>
	[HttpGet]
	[Route("/")]
	public IActionResult Index()
	{
		if (!IsUserAuthenticated && ServerSession.Password is not null)
		{
			return View("../User/LogInPassword");
		}

		if (!IsUserAuthenticated)
		{
			return View("../User/LogIn");
		}

		ViewData["PageTitle"] = $"{ServerSession.HostName} - HTTP Share";

		bool sendSession = ServerSession is ISendSession,
			receiveSession = ServerSession is IReceiveSession;

		ViewData["SendSession"] = sendSession;
		ViewData["ReceiveSession"] = receiveSession;

		if (sendSession) ViewData["OutboxFiles"] = (ServerSession as ISendSession)!.OutboxFiles;

		return View();
	}
}

// TODO Add implementation of the password system in this controller later
