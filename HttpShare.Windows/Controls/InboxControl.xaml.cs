// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using HttpShare.Files;
using HttpShare.Windows.DataContexts;

using Microsoft.Win32;

namespace HttpShare.Windows.Controls;

/// <summary>
/// The code behind class for InboxControl.
/// </summary>
public partial class InboxControl : UserControl
{
	/// <summary>
	/// The data context object parsed to <see cref="InboxControlDataContext"/>.
	/// </summary>
	private InboxControlDataContext ParsedDataContext => (InboxControlDataContext) DataContext;


	/// <summary>
	/// The class constructor.
	/// </summary>
	public InboxControl()
	{
		InitializeComponent();
	}


	/// <summary>
	/// Updates the inbox files list.
	/// </summary>
	/// <param name="dispatcher">The control <see cref="Dispatcher"/> object.</param>
	/// <param name="IInboxFiles">The current inbox file collection.</param>
	public void AddIInboxFiles(Dispatcher dispatcher, IEnumerable<IInboxFile> IInboxFiles)
	{
		dispatcher.Invoke(() =>
		{
			foreach (IInboxFile file in IInboxFiles) ParsedDataContext.IInboxFiles.Add(file);
			ParsedDataContext.InvokePropertyChangedEvent();
		});
	}


	/// <summary>
	/// Handles the "Download All..." button on click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
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

	/// <summary>
	/// Handles the download folder selector dialog OK event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	private void OnFolderOkOpenFolderDialog(object? sender, CancelEventArgs _)
	{
		string selectedFolder = (sender as OpenFolderDialog)!.FolderName;

		try
		{
			foreach (IInboxFile file in ParsedDataContext.IInboxFiles)
			{
				string destination = Path.Combine(selectedFolder, file.Name);
				using FileStream fileStream = File.OpenWrite(destination);

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
