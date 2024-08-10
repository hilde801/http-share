using System.Collections.Generic;

namespace HttpShare;

public interface IReceiveSession
{
	public delegate void ReceivedFilesHandler(ICollection<InboxFile> files);

	public event ReceivedFilesHandler? OnReceivedFiles;


	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files);
}
