using HttpShare.Files;

namespace HttpShare.Windows.Files;

public static class OutboxFileExtensions
{
	public static OutboxFile ToOutboxFile(this IOutboxFileListItem input)
	{
		return new OutboxFile(input.FilePath, input.Data);
	}

	public static OutboxFileListItem ToOutboxFileListItem(this IOutboxFile input)
	{
		return new OutboxFileListItem(input.FilePath, input.Data);
	}
}
