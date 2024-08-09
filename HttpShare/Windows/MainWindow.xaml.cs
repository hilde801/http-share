using HttpShare.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

			WebApplicationBuilder builder = WebApplication.CreateBuilder();
			builder.Services.AddControllersWithViews();
			builder.Services.AddSingleton<ServerSession>(dualSession);

			WebApplication = builder.Build();
			WebApplication.MapControllers();

			WebApplication.Urls.Add("http://*:80");

			await WebApplication.StartAsync();
		}

		else await WebApplication.DisposeAsync();
	}

	protected async override void OnClosing(CancelEventArgs e)
	{
		if (ParsedDataContext.IsServerRunning) await WebApplication.DisposeAsync();
		base.OnClosing(e);
	}
}
