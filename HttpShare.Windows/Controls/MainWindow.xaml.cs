// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

using HttpShare.Files;
using HttpShare.Servers;
using HttpShare.Windows.DataContexts;

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
	/// The instance of <see cref="DualModeServer"/>.
	/// </summary>
	private DualModeServer? DualModeServer { get; set; } = null;


	/// <summary>
	/// The class constructor.
	/// </summary>
	public MainWindow()
	{
		InitializeComponent();

		serverOptionsControl.ServerOptions.PropertyChanged += OnChangeServerOptions;
	}

	private void OnChangeServerOptions(object? sender, PropertyChangedEventArgs e)
	{
		ParsedDataContext.EnableServerToggleButton = !serverOptionsControl.ServerOptions.HasErrors;
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
			DualModeServer = new DualModeServer(80, outboxControl.OutboxFiles);
			DualModeServer.ReceiveFile += OnReceivedFiles;

			ServerStartWindow serverStartWindow = new ServerStartWindow { Owner = this };
			serverStartWindow.Show();

			await DualModeServer!.StartAsync();
		}

		else await DualModeServer!.DisposeAsync();

		outboxControl.IsEnabled = !ParsedDataContext.IsServerRunning;

	}

	/// <summary>
	/// The handler method for file received event.
	/// </summary>
	/// <param name="files">The received <see cref="IInboxFile"/> collection object.</param>
	private void OnReceivedFiles(IEnumerable<IInboxFile> files)
	{
		inboxControl.AddIInboxFiles(Dispatcher, files);
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
