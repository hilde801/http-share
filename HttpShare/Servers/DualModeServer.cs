using HttpShare.Controllers;
using HttpShare.Sessions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HttpShare.Servers;

public sealed class DualModeServer : IAsyncDisposable
{
	private WebApplication App { get; }


	public DualModeServer(int port, ICollection<OutboxFile> outboxFiles)
	{
		DualSession dualSession = new DualSession(outboxFiles);

		WebApplicationBuilder builder = WebApplication.CreateBuilder();

		builder.Services.AddControllersWithViews()
			.AddApplicationPart(typeof(HomeController).Assembly)
			.AddControllersAsServices();

		builder.Services.AddSingleton(dualSession);

		App = builder.Build();
		App.MapControllers();


		App.Urls.Add($"http://*:{port}");
	}


	public Task StartAsync() => App.StartAsync();

	public ValueTask DisposeAsync() => App.DisposeAsync();
}
