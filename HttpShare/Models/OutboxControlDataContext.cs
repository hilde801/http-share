// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HttpShare.Models;


/// <summary>
/// The data context class for <see cref="Controls.OutboxControl"/>.
/// </summary>
public sealed class OutboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	/// <summary>
	/// A collection of <see cref="File"/>s to be sent to connected clients.
	/// </summary>
	public ObservableCollection<File> OutboxFiles { get; } = [];


	/// <summary>
	/// Represents if this control is enabled or not.
	/// </summary>
	private bool isEnabled = true;

	/// <summary>
	/// Represents if this control is enabled or not.
	/// </summary>
	public bool IsEnabled
	{
		get => isEnabled;

		set
		{
			isEnabled = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
		}
	}
}
