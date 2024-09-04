using HttpShare.Files;

namespace HttpShare.Windows.Files;

internal static class OutboxFileExtensions
{
	public static OutboxFile ToOutboxFile(this IOutboxFileListItem input)
	{
		return new OutboxFile(input.FilePath, input.Data);
	}

	public static OutboxFile ToOutboxFileListItem(this IOutboxFile input)
	{
		return new OutboxFileListItem(input.FilePath, input.Data);
	}
}
