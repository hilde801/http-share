// =============================================================================
// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share
// =============================================================================

using System;

namespace HttpShare;

public abstract class ServerSession
{
	public string HostName => $"{Environment.MachineName}/{Environment.UserName}";
}
