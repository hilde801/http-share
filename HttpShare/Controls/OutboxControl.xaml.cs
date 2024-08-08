using HttpShare.Models;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace HttpShare.Controls;

public partial class OutboxControl : UserControl
{
	private OutboxControlDataContext ParsedDataContext => (OutboxControlDataContext)DataContext;


	public OutboxControl()
	{
		InitializeComponent();
	}


	private void OnClickAddFilesButton(object? sender, RoutedEventArgs _)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Multiselect = true,
			CheckFileExists = true,
			CheckPathExists = true
		};

		// TODO Add something here later

		openFileDialog.ShowDialog();
	}

	private void OnClickClearOutboxButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.OutboxFiles.Clear();
	}
}
