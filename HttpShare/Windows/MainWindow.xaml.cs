using HttpShare.Models;
using Microsoft.AspNetCore.Builder;
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
			WebApplicationBuilder builder = WebApplication.CreateBuilder();

			WebApplication = builder.Build();

			// TODO For testing purposes only - remove this later
			WebApplication.Map("/", () => "Hello, World!");

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
