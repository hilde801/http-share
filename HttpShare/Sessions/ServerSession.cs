// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare.Sessions;

/// <summary>
/// This class is the base to describe server session classes.
/// All classes that inherit this class will be used as a session type.
/// </summary>
public abstract partial class ServerSession
{
	public delegate void ServerSessionEventHandler(ServerSessionEvent serverEvent);

	public event ServerSessionEventHandler? ServerEvent;

	internal void InvokeServerEvent(ServerSessionEvent serverEvent) => ServerEvent?.Invoke(serverEvent);


	/// <summary>
	/// Gets the host's display name.
	/// </summary>
	/// <returns>
	/// The host's display name in the "(hostname)/(username)" format.
	/// </returns>
	public string HostName => $"{Environment.MachineName}/{Environment.UserName}";


	public string? Password { get; set; } = null;
}
