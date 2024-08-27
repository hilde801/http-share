// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

using HttpShare.Models;

using Microsoft.Win32;

using WindowsControls = System.Windows.Controls;

namespace HttpShare.Controls;

/// <summary>
/// The code behind class for OutboxControl.
/// </summary>
public partial class OutboxControl : WindowsControls.UserControl
{
	/// <summary>
	/// The data context object parsed to <see cref="OutboxControlDataContext"/>.
	/// </summary>
	private OutboxControlDataContext ParsedDataContext => (OutboxControlDataContext) DataContext;


	/// <summary>
	/// A collection of <see cref="File"/>s to be sent to the clients.
	/// </summary>
	public ICollection<File> OutboxFiles => ParsedDataContext.OutboxFiles;


	/// <summary>
	/// The class constructor.
	/// </summary>
	public OutboxControl()
	{
		InitializeComponent();
	}


	/// <summary>
	/// Handles the add outbox files dialog on file OK event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	private void OnFileOkAddFilesDialog(object? sender, CancelEventArgs _)
	{
		OpenFileDialog addFilesDialog = (OpenFileDialog) sender!;
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

	/// <summary>
	/// Handles the "Add Files..." button on click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
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

	/// <summary>
	/// Handles the "Clear Outbox" button on click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	private void OnClickClearOutboxButton(object? sender, RoutedEventArgs _)
	{
		ParsedDataContext.OutboxFiles.Clear();
	}
}
