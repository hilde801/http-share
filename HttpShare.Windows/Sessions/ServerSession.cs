// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System;

namespace HttpShare.Sessions;

/// <summary>
/// This class is the base to describe server session classes.
/// All classes that inherit this class will be used as a session type.
/// </summary>
public abstract class ServerSession
{
	/// <summary>
	/// Gets the host's display name.
	/// </summary>
	/// <returns>
	/// The host's display name in the "(hostname)/(username)" format.
	/// </returns>
	public string HostName => $"{Environment.MachineName}/{Environment.UserName}";
}
