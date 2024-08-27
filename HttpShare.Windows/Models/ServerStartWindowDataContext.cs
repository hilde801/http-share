// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HttpShare.Models;

/// <summary>
/// The data context class for <see cref="Windows.ServerStartWindow" />
/// </summary>
public sealed class ServerStartWindowDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	/// <summary>
	/// The address of the server.
	/// </summary>
	private string serverAddress = string.Empty;

	/// <summary>
	/// The address of the server.
	/// </summary>
	public string ServerAddress
	{
		get => serverAddress;

		set
		{
			serverAddress = value;
			InvokePropertyChangedEvent();
		}
	}


	/// <summary>
	/// Invokes <see cref="PropertyChanged"/>.
	/// </summary>
	/// <param name="propertyName">The name of the invoking property.</param>
	private void InvokePropertyChangedEvent([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
