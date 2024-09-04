// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using HttpShare.Files;

namespace HttpShare.Sessions;

/// <summary>
/// Describes a server session that supports both sending and receiving files. 
/// </summary>
public sealed class DualSession(IEnumerable<IOutboxFile> outboxFiles) : ServerSession,
	ISendSession, IReceiveSession
{
	/// <summary>
	///  A collection of files to the sent to client devices.
	/// </summary>
	public IEnumerable<IOutboxFile> OutboxFiles => outboxFiles;

	/// <summary>
	/// The implementation of <see cref="IReceiveSession.OnReceivedFiles"/>.
	/// </summary>
	public event IReceiveSession.ReceivedFilesHandler? OnReceivedFiles;


	/// <summary>
	/// Invokes <see cref="OnReceivedFiles"/>.
	/// </summary>
	/// <param name="files">Files received from client devices.</param>
	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files)
	{
		OnReceivedFiles?.Invoke(files);
	}
}
