using System.Collections.Generic;

namespace HttpShare;

public interface IReceiveSession
{
	public delegate void FileReceivedHandler(ICollection<File> files);

	public event FileReceivedHandler? FileReceived;


	public void InvokeFileReceived(ICollection<File> files);
}
