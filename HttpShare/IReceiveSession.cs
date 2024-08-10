using System.Collections.Generic;

namespace HttpShare;

public interface IReceiveSession
{
	public delegate void FileReceivedHandler(ICollection<InboxFile> files);

	public event FileReceivedHandler? FilesReceived;


	public void InvokeFilesReceived(ICollection<InboxFile> files);
}
