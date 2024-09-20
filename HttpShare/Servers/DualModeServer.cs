// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Security.Claims;

using HttpShare.Controllers;
using HttpShare.Files;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace HttpShare.Servers;

/// <summary>
/// A wrapper class to create a <see cref="DualSession"/> based server.
/// </summary>
public sealed class DualModeServer : IAsyncDisposable
{
	// ================================================================================ //
	// TODO Move these stuff into a separate parent class later
	// ================================================================================ //
	public delegate void ServerEventHandler();

	public delegate void ServerExceptionEventHandler(Exception exception);


	public event ServerEventHandler? ServerStarted, ServerEnded;

	public event ServerExceptionEventHandler? ServerException;
	// ================================================================================ //


	/// <summary>
	/// The <see cref="WebApplication"/> instance.
	/// </summary>
	private WebApplication App { get; }


	public DualSession DualSession { get; }


	private CancellationTokenSource CancellationTokenSource { get; }


	/// <summary>
	/// Initializes an instance of <see cref="DualModeServer"/>.
	/// </summary>
	/// <param name="port">The port to be used.</param>
	/// <param name="outboxFiles">A collection of files to be sent to client devices.</param>
	public DualModeServer(int port, IEnumerable<IOutboxFile> outboxFiles, string? password = null)
	{
		CancellationTokenSource = new CancellationTokenSource();

		DualSession = new DualSession(outboxFiles) { Password = password };

		WebApplicationBuilder builder = WebApplication.CreateBuilder();

		builder.Services.AddControllersWithViews()
			.AddApplicationPart(typeof(HomeController).Assembly);

		builder.Services
			.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				options.LoginPath = "/LogIn/";

				options.Cookie.IsEssential = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			});

		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy(Constants.LoggedInUsersOnlyPolicy, policy =>
			{
				policy.RequireClaim(ClaimTypes.Name);
			});
		});

		builder.Services.AddAntiforgery();
		builder.Services.AddSingleton<ServerSession>(DualSession);

		builder.WebHost.ConfigureKestrel(ConfigureKestrel);


		App = builder.Build();

		App.UseAuthentication();
		App.MapControllers();

		App.Urls.Add($"http://*:{port}");
	}


	/// <summary>
	/// Starts the server.
	/// </summary>
	/// <returns></returns>
	[Obsolete]
	public Task StartAsync() => App.RunAsync();

	/// <summary>
	/// Stop and dispose the server.
	/// </summary>
	/// <returns></returns>
	[Obsolete]
	public ValueTask DisposeAsync() => App.DisposeAsync();


	public void Start()
	{
		new Thread(A1).Start();
		ServerStarted?.Invoke();
	}

	public void Stop() => CancellationTokenSource.Cancel();




	/// <summary>
	/// Invoke <see cref="ReceiveFile"/>.
	/// </summary>
	/// <param name="inboxFiles">A collection of files received from the client.</param>
	//private void HandleReceivedFiles(ICollection<IInboxFile> inboxFiles) => ReceiveFile?.Invoke(inboxFiles);


	private void ConfigureKestrel(KestrelServerOptions options)
	{
		options.Limits.MaxRequestBodySize = long.MaxValue;
	}


	private void A1()
	{
		CancellationTokenSource.Token.Register(A2);

		try
		{
			App.Run();
		}

		catch (Exception exception)
		{
			ServerException?.Invoke(exception);
		}
	}

	private async void A2()
	{
		CancellationTokenSource.Dispose();

		await App.StopAsync();
		await App.DisposeAsync();

		ServerEnded?.Invoke();
	}
}
