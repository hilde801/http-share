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
