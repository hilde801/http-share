// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.ObjectModel;
using System.ComponentModel;

using HttpShare.Files;

namespace HttpShare.Windows.DataContexts;

/// <summary>
/// The data context class for <see cref="Controls.InboxControl"/>.
/// </summary>
public sealed class InboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	/// <summary>
	/// Invokes <see cref="PropertyChanged"/>.
	/// </summary>
	public void InvokePropertyChangedEvent()
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
	}


	/// <summary>
	/// A collection of incoming files for the "Inbox" tab list.
	/// </summary>
	public ObservableCollection<IInboxFile> IInboxFiles { get; } = [];

	/// <summary>
	/// Represents whether the "Download All..." button is enabled or not.
	/// </summary>
	public bool IsDownloadAllButtonEnabled => IInboxFiles.Count > 0;
}
