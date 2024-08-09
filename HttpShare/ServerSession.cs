using System;

namespace HttpShare;

public abstract class ServerSession
{
	public string HostName => $"{Environment.MachineName}/{Environment.UserName}";
}
