using HttpShare.Files;

namespace HttpShare.Windows.Files;

internal interface IOutboxFileListItem : IOutboxFile
{
	public bool IsSelected { get; set; }
}
