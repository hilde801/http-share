using System.IO;

using HttpShare.Files;

namespace HttpShare.Windows.Files;

public sealed record OutboxFileListItem(string FilePath, byte[] Data) : IOutboxFile, IListItemSelectable
{
	public bool IsSelected { get; set; } = false;

	public string Name => Path.GetFileName(FilePath);

	public long Length => Data.LongLength;


	public static IOutboxFile Load(string filePath)
	{
		byte[] buffer = File.ReadAllBytes(filePath);
		return new OutboxFileListItem(filePath, buffer);
	}

	public static IOutboxFile Load(FileStream fileStream)
	{
		if (fileStream.CanSeek) fileStream.Seek(0, SeekOrigin.Begin);

		using MemoryStream buffer = new MemoryStream();
		fileStream.CopyTo(buffer);

		return new OutboxFileListItem(fileStream.Name, buffer.ToArray());
	}
}
