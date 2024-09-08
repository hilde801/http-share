using System.Security.Claims;

using HttpShare.Sessions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HttpShare.Controllers;

public abstract class CustomController(ServerSession serverSession) : Controller
{
	public const string DisplayNameClaim = "DisplayName";

	public const string PasswordClaim = "Password";


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

			Claim? passwordClaim = User.Claims.FirstOrDefault(PasswordPredicate);
			if (passwordClaim is null) return false;

			return passwordClaim.Value != serverSession.Password;
		}
	}


	protected async Task UserLogIn(string displayName)
	{
		Claim[] claims = [new Claim(DisplayNameClaim, displayName)];

		ClaimsIdentity identity = new ClaimsIdentity(claims,
			CookieAuthenticationDefaults.AuthenticationScheme);

		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
	}


	private bool DisplayNamePredicate(Claim claim)
	{
		return claim.Type.Equals(DisplayNameClaim, StringComparison.InvariantCultureIgnoreCase);
	}

	private bool PasswordPredicate(Claim claim)
	{
		return claim.Type.Equals(PasswordClaim, StringComparison.InvariantCultureIgnoreCase);
	}
}
