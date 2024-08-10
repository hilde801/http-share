using HttpShare.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HttpShare.Controls;

public partial class InboxControl : UserControl
{
	private InboxControlDataContext ParsedDataContext => (InboxControlDataContext)DataContext;


	public InboxControl()
	{
		InitializeComponent();
	}

	public void AddInboxFiles(Dispatcher dispatcher, ICollection<InboxFile> inboxFiles)
	{
		dispatcher.Invoke(() =>
		{
			foreach (InboxFile file in inboxFiles) ParsedDataContext.InboxFiles.Add(file);
		});
	}


	public void OnClickDownloadAllButton(object? sender, RoutedEventArgs _)
	{
		OpenFolderDialog openFolderDialog = new OpenFolderDialog
		{
			Multiselect = false,
			InitialDirectory = Path.Combine("C:", "Users", Environment.UserName, "Downloads"),
		};

		openFolderDialog.FolderOk += OnFolderOkOpenFolderDialog;
		openFolderDialog.ShowDialog();
	}

	private void OnFolderOkOpenFolderDialog(object? sender, CancelEventArgs e)
	{
		string selectedFolder = (sender as OpenFolderDialog)!.FolderName;

		try
		{
			foreach (InboxFile file in ParsedDataContext.InboxFiles)
			{
				string destination = Path.Combine(selectedFolder, file.Filename);
				using FileStream fileStream = System.IO.File.OpenWrite(destination);

				fileStream.Write(file.Data);
				fileStream.Flush();
			}

			MessageBox.Show($"Successfully saved all files to \"{selectedFolder}\"!", "Success",
				MessageBoxButton.OK, MessageBoxImage.Information);
		}

		catch (Exception exception)
		{
			MessageBox.Show(exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			Application.Current.Shutdown();
		}
	}
}
