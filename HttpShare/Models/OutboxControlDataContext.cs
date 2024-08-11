// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HttpShare.Models;

public sealed class OutboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public ObservableCollection<File> OutboxFiles { get; } = [];



	private bool isEnabled = true;

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
