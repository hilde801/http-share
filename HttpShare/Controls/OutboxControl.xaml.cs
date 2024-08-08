using HttpShare.Models;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
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


	private void OnFileOkAddFilesDialog(object? sender, CancelEventArgs _)
	{
		OpenFileDialog addFilesDialog = (OpenFileDialog)sender!;
		Stream[] fileStreams = addFilesDialog.OpenFiles();

		foreach (FileStream fileStream in fileStreams)
		{
			string path = fileStream.Name;
			byte[] data = null!;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				fileStream.CopyTo(memoryStream);
				data = memoryStream.ToArray();
			}

			fileStream.Dispose();

			ParsedDataContext.OutboxFiles.Add(new File(path, data));
		}
	}

	private void OnClickAddFilesButton(object? sender, RoutedEventArgs _)
	{
		OpenFileDialog addFilesDialog = new OpenFileDialog
		{
			Multiselect = true,
			CheckFileExists = true,
			CheckPathExists = true
		};

		addFilesDialog.FileOk += OnFileOkAddFilesDialog;
		addFilesDialog.ShowDialog();
	}

	private void OnClickClearOutboxButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.OutboxFiles.Clear();
	}
}
