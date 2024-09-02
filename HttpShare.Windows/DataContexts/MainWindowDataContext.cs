// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.ComponentModel;

using HttpShare.Windows.Controls;

namespace HttpShare.Windows.DataContexts;

/// <summary>
/// The data context class for <see cref="MainWindow"/>. 
/// </summary>
public sealed class MainWindowDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	/// <summary>
	/// Represents if the server is running or not.
	/// </summary>
	private bool isServerRunning = false;


	/// <summary>
	/// Represents if the server is running or not. 
	/// </summary>
	public bool IsServerRunning
	{
		get => isServerRunning;

		set
		{
			isServerRunning = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}

	/// <summary>
	/// If the server is running, this property will return "Stop Server".
	/// Otherwise, this property will return "Start Server".
	/// </summary>
	public string ServerToggleButtonText => IsServerRunning ? "Stop Server" : "Start Server";

}
