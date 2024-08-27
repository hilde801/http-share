// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using HttpShare.Servers;
using HttpShare.Windows.Models;

namespace HttpShare.Windows.Controls;

/// <summary>
/// The code-behind class for the main application window.
/// </summary>
public partial class MainWindow : Window
{
	/// <summary>
	/// The data context object parsed to <see cref="MainWindowDataContext"/>.
	/// </summary>
	private MainWindowDataContext ParsedDataContext => (MainWindowDataContext) DataContext;


	/// <summary>
	/// The web application property.
	/// </summary>
	// private WebApplication WebApplication { get; set; } = null!;


	private DualModeServer? DualModeServer { get; set; } = null;


	/// <summary>
	/// The class constructor.
	/// </summary>
	public MainWindow()
	{
		InitializeComponent();
	}


	/// <summary>
	/// The handler method for the server toggle button on click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	private async void OnClickServerToggleButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.IsServerRunning = !ParsedDataContext.IsServerRunning;

		if (ParsedDataContext.IsServerRunning)
		{
			/*DualSession dualSession = new DualSession(outboxControl.OutboxFiles);
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
			WebApplication.Urls.Add("http://127.0.0.1:80");*/

			DualModeServer = new DualModeServer(80, outboxControl.OutboxFiles);

			ServerStartWindow serverStartWindow = new ServerStartWindow { Owner = this };
			serverStartWindow.Show();

			await DualModeServer!.StartAsync();
		}

		else await DualModeServer!.StartAsync();

		outboxControl.IsEnabled = !ParsedDataContext.IsServerRunning;

	}

	/// <summary>
	/// The handler method for file received event.
	/// </summary>
	/// <param name="files">The received <see cref="InboxFile"/> collection object.</param>
	private void OnReceivedFiles(ICollection<InboxFile> files)
	{
		inboxControl.AddInboxFiles(Dispatcher, files);
	}

	/// <summary>
	/// The handler method for file received event. 
	/// </summary>
	protected async override void OnClosing(CancelEventArgs e)
	{
		if (ParsedDataContext.IsServerRunning && DualModeServer != null)
		{
			await DualModeServer.DisposeAsync();
		}

		base.OnClosing(e);
	}
}
