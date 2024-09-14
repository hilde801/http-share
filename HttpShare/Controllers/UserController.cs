using HttpShare.Sessions;

using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

[Controller]
public sealed class UserController(ServerSession serverSession) : CustomController(serverSession)
{
	public const string ErrorsKey = "Errors";

	[HttpGet]
	[Route("/LogIn/")]
	public IActionResult LogIn()
	{
		if (!string.IsNullOrEmpty(ServerSession.Password) ||
			!string.IsNullOrWhiteSpace(ServerSession.Password)) return View("../User/LogInPassword");

		else return View("../User/LogIn");
	}

	[HttpPost]
	[Route("/LogIn/")]
	public async Task<IActionResult> LogIn([FromForm] string displayName, [FromForm] string password)
	{
		if (!string.IsNullOrEmpty(ServerSession.Password)
			|| !string.IsNullOrWhiteSpace(ServerSession.Password))
		{
			if (!password.Equals(ServerSession.Password))
			{
				string[] errorMessages = ["Invalid password."];

				ViewData[ErrorsKey] = errorMessages;
				return View("../User/LogInPassword");
			}
		}

		await UserLogIn(displayName);
		return RedirectPermanent("/");
	}
}
