using System.Security.Claims;

using HttpShare.Sessions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

public abstract class CustomController(ServerSession serverSession) : Controller
{
	protected ServerSession ServerSession => serverSession;

	protected string? DisplayName
	{
		get => User.Claims.FirstOrDefault(DisplayNamePredicate)?.Value;
	}

	protected bool IsUserAuthenticated
	{
		get
		{
			if (serverSession.Password is null) return false;

			Claim? displayNameClaim = User.Claims.FirstOrDefault(DisplayNamePredicate);
			if (displayNameClaim is null) return false;

			return displayNameClaim.Value != serverSession.Password;
		}
	}


	protected async Task UserLogIn(string displayName)
	{
		Claim[] claims = [new Claim(ClaimTypes.Name, displayName)];

		ClaimsIdentity identity = new ClaimsIdentity(claims,
			CookieAuthenticationDefaults.AuthenticationScheme);

		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
	}


	private bool DisplayNamePredicate(Claim claim)
	{
		return claim.Type
			.Equals(ClaimTypes.Name, StringComparison.InvariantCultureIgnoreCase);
	}
}
