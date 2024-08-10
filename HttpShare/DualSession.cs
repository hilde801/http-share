using System.Collections.Generic;

namespace HttpShare;

public sealed class DualSession(ICollection<File> outboxFiles) : ServerSession,
	ISendSession, IReceiveSession
{
	public ICollection<File> OutboxFiles => outboxFiles;

	public event IReceiveSession.FileReceivedHandler? FilesReceived;


	public void InvokeFilesReceived(ICollection<File> files)
	{
		FilesReceived?.Invoke(files);
	}
}
