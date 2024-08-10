using HttpShare.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace HttpShare.Windows;

public partial class MainWindow : Window
{
	private MainWindowDataContext ParsedDataContext => (MainWindowDataContext)DataContext;


	private WebApplication WebApplication { get; set; } = null!;


	public MainWindow()
	{
		InitializeComponent();
	}


	private async void OnClickServerToggleButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.IsServerRunning = !ParsedDataContext.IsServerRunning;

		if (ParsedDataContext.IsServerRunning)
		{
			DualSession dualSession = new DualSession(outboxControl.OutboxFiles);
			dualSession.OnReceivedFiles += OnReceivedFiles;

			WebApplicationBuilder builder = WebApplication.CreateBuilder();
			builder.Services.AddControllersWithViews();
			builder.Services.AddSingleton<ServerSession>(dualSession);

			builder.WebHost.ConfigureKestrel(options =>
			{
				options.Limits.MaxRequestBodySize = long.MaxValue;
			});


			WebApplication = builder.Build();
			WebApplication.MapControllers();

			WebApplication.Urls.Add("http://192.168.*:80");
			WebApplication.Urls.Add("http://127.0.0.1:80");

			await WebApplication.StartAsync();
		}

		else await WebApplication.DisposeAsync();

		outboxControl.IsEnabled = !ParsedDataContext.IsServerRunning;

	}

	private void OnReceivedFiles(ICollection<InboxFile> files)
	{
		inboxControl.AddInboxFiles(Dispatcher, files);
	}

	protected async override void OnClosing(CancelEventArgs e)
	{
		if (ParsedDataContext.IsServerRunning) await WebApplication.DisposeAsync();
		base.OnClosing(e);
	}
}
