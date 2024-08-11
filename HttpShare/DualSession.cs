// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;

namespace HttpShare;

public sealed class DualSession(ICollection<File> outboxFiles) : ServerSession,
	ISendSession, IReceiveSession
{
	public ICollection<File> OutboxFiles => outboxFiles;

	public event IReceiveSession.ReceivedFilesHandler? OnReceivedFiles;


	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files)
	{
		OnReceivedFiles?.Invoke(files);
	}
}
