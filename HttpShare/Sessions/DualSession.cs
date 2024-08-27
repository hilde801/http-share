// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare.Sessions;

/// <summary>
/// Describes a server session that supports both sending and receiving files. 
/// </summary>
public sealed class DualSession(ICollection<OutboxFile> outboxFiles) : ServerSession,
	ISendSession, IReceiveSession
{
	public ICollection<OutboxFile> OutboxFiles => outboxFiles;

	public event IReceiveSession.ReceivedFilesHandler? OnReceivedFiles;


	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files)
	{
		OnReceivedFiles?.Invoke(files);
	}
}
