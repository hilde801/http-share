using System.Collections.Generic;

namespace HttpShare;

public sealed class DualSession(ICollection<File> outboxFiles) : ServerSession,
	ISendSession, IReceiveSession
{
	public ICollection<File> OutboxFiles => outboxFiles;

	public event IReceiveSession.FileReceivedHandler? FileReceived;


	public void InvokeFileReceived(ICollection<File> files)
	{
		FileReceived?.Invoke(files);
	}
}
