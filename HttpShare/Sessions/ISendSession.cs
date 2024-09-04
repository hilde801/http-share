// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using HttpShare.Files;

namespace HttpShare.Sessions;

/// <summary>
/// Describes a send session. 
/// </summary>
public interface ISendSession
{
	/// <summary>
	/// A collection of <see cref="OutboxFile"/>s to be sent to connected clients.
	/// </summary>
	public IEnumerable<IOutboxFile> OutboxFiles { get; }
}
