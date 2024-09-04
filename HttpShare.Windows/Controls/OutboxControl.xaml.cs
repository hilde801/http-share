// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using HttpShare.Files;
using HttpShare.Windows.DataContexts;
using HttpShare.Windows.Files;

using Microsoft.Win32;

namespace HttpShare.Windows.Controls;

/// <summary>
/// The code behind class for OutboxControl.
/// </summary>
public partial class OutboxControl : UserControl
{
	/// <summary>
	/// The data context object parsed to <see cref="OutboxControlDataContext"/>.
	/// </summary>
	private OutboxControlDataContext ParsedDataContext => (OutboxControlDataContext) DataContext;


	/// <summary>
	/// A collection of <see cref="File"/>s to be sent to the clients.
	/// </summary>
	public IEnumerable<IOutboxFile> OutboxFiles => ParsedDataContext.OutboxFiles.Cast<IOutboxFile>();


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

		foreach (FileStream fileStream in fileStreams.Cast<FileStream>())
		{
			ParsedDataContext.OutboxFiles.Add(OutboxFile.Load(fileStream).ToOutboxFileListItem());
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
