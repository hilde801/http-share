// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare.Sessions;

/// <summary>
/// This class is the base to describe server session classes.
/// All classes that inherit this class will be used as a session type.
/// </summary>
public abstract class ServerSession
{
	public enum ServerEventType : int { Error, Information }

	public delegate void ServerEventHandler(ServerEventType type, string message);

	public event ServerEventHandler? ServerEvent;

	public void InvokeServerEvent(ServerEventType type, string message) => ServerEvent?.Invoke(type, message);


	/// <summary>
	/// Gets the host's display name.
	/// </summary>
	/// <returns>
	/// The host's display name in the "(hostname)/(username)" format.
	/// </returns>
	public string HostName => $"{Environment.MachineName}/{Environment.UserName}";


	public string? Password { get; set; } = null;
}
