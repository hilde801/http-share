namespace HttpShare.Windows.Files;

public class OutboxFileListItem(string filePath, byte[] data) : OutboxFile(filePath, data), IOutboxFileListItem
{
	public bool IsSelected { get; set; } = false;
}
