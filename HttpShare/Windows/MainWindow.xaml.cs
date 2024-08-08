using HttpShare.Models;
using System.Windows;

namespace HttpShare.Windows;

public partial class MainWindow : Window
{
	private MainWindowDataContext ParsedDataContext => (MainWindowDataContext)DataContext;

	public MainWindow()
	{
		InitializeComponent();
	}


	private void OnClickServerToggleButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.IsServerRunning = !ParsedDataContext.IsServerRunning;
	}
}
