// =============================================================================
// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share
// =============================================================================

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HttpShare.Models;

public sealed class InboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public void InvokePropertyChangedEvent()
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
	}


	public ObservableCollection<InboxFile> InboxFiles { get; } = [];

	public bool IsDownloadAllButtonEnabled => InboxFiles.Count > 0;
}
