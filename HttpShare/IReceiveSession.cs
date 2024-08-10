// =============================================================================
// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share
// =============================================================================

using System.Collections.Generic;

namespace HttpShare;

public interface IReceiveSession
{
	public delegate void ReceivedFilesHandler(ICollection<InboxFile> files);

	public event ReceivedFilesHandler? OnReceivedFiles;


	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files);
}
