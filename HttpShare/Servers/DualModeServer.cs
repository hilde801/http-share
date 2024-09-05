// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using HttpShare.Controllers;
using HttpShare.Files;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HttpShare.Servers;

/// <summary>
/// A wrapper class to create a <see cref="DualSession"/> based server.
/// </summary>
public sealed class DualModeServer : IAsyncDisposable
{
	/// <summary>
	/// The delegate of <see cref="ReceiveFile"/> handler. 
	/// </summary>
	/// <param name="inboxFiles"></param>
	public delegate void ReceiveFilesHandler(ICollection<IInboxFile> inboxFiles);

	/// <summary>
	/// Invoked when the the server receive files from client devices. 
	/// </summary>
	public event ReceiveFilesHandler? ReceiveFile;

	/// <summary>
	/// The <see cref="WebApplication"/> instance.
	/// </summary>
	private WebApplication App { get; }


	/// <summary>
	/// Initializes an instance of <see cref="DualModeServer"/>.
	/// </summary>
	/// <param name="port">The port to be used.</param>
	/// <param name="outboxFiles">A collection of files to be sent to client devices.</param>
	public DualModeServer(int port, IEnumerable<IOutboxFile> outboxFiles, string? password = null)
	{
		DualSession dualSession = new DualSession(outboxFiles) { Password = password };
		dualSession.OnReceivedFiles += HandleReceivedFiles;

		WebApplicationBuilder builder = WebApplication.CreateBuilder();

		builder.Services.AddControllersWithViews()
			.AddApplicationPart(typeof(HomeController).Assembly);

		builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = long.MaxValue);

		builder.Services.AddSingleton<ServerSession>(dualSession);


		App = builder.Build();

		App.MapControllers();

		App.Urls.Add($"http://*:{port}");
	}


	/// <summary>
	/// Starts the server.
	/// </summary>
	/// <returns></returns>
	public Task StartAsync() => App.RunAsync();

	/// <summary>
	/// Stop and dispose the server.
	/// </summary>
	/// <returns></returns>
	public ValueTask DisposeAsync() => App.DisposeAsync();


	/// <summary>
	/// Invoke <see cref="ReceiveFile"/>.
	/// </summary>
	/// <param name="inboxFiles">A collection of files received from the client.</param>
	private void HandleReceivedFiles(ICollection<IInboxFile> inboxFiles) => ReceiveFile?.Invoke(inboxFiles);
}
