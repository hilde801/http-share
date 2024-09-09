using HttpShare.Files;

namespace HttpShare.Windows.Files;

public sealed record InboxFileListItem(string SenderName, string Name, byte[] Data) : IInboxFile, IListItemSelectable
{
	public bool IsSelected { get; set; } = false;


	public long Length => Data.LongLength;
}
