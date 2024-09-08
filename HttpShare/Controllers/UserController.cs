using HttpShare.Models;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

[Controller]
public sealed class UserController(ServerSession serverSession) : CustomController(serverSession)
{
	public const string ErrorsKey = "Errors";

	[HttpPost]
	[Route("/LogIn/")]
	public async Task<IActionResult> UserLogIn([FromForm] LogInModel logInModel)
	{
		if (ServerSession.Password == null) return RedirectPermanent("/");

		if (logInModel.Password != ServerSession.Password)
		{
			string[] errorMessages = ["Invalid password."];

			ViewData[ErrorsKey] = errorMessages;
			return View("Password");
		}

		await UserLogIn(logInModel.DisplayName);
		return RedirectPermanent("/");
	}
}
