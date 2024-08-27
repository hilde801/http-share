using HttpShare.Controllers;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HttpShare.Servers;

public sealed class DualModeServer : IAsyncDisposable
{
	public delegate void ReceiveFilesHandler(ICollection<InboxFile> inboxFiles);

	public event ReceiveFilesHandler? ReceiveFile;


	private WebApplication App { get; }


	public DualModeServer(int port, ICollection<OutboxFile> outboxFiles)
	{
		DualSession dualSession = new DualSession(outboxFiles);
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


	public Task StartAsync() => App.RunAsync();

	public ValueTask DisposeAsync() => App.DisposeAsync();


	private void HandleReceivedFiles(ICollection<InboxFile> inboxFiles) => ReceiveFile?.Invoke(inboxFiles);
}
