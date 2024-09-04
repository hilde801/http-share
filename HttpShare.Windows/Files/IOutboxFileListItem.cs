using HttpShare.Files;

namespace HttpShare.Windows.Files;

public interface IOutboxFileListItem : IOutboxFile
{
	public bool IsSelected { get; set; }
}
