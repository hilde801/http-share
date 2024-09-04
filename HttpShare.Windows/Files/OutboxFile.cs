using System.IO;

using HttpShare.Files;

namespace HttpShare.Windows.Files;

public class OutboxFile(string filePath, byte[] data) : IOutboxFile
{
	public string FilePath => filePath;

	public byte[] Data => data;

	public string Name => Path.GetFileName(FilePath);

	public long Length => Data.LongLength;


	public static OutboxFile Load(string filePath)
	{
		byte[] buffer = File.ReadAllBytes(filePath);
		return new OutboxFile(filePath, buffer);
	}

	public static OutboxFile Load(FileStream fileStream)
	{
		if (fileStream.CanSeek) fileStream.Seek(0, SeekOrigin.Begin);

		using MemoryStream buffer = new MemoryStream();
		fileStream.CopyTo(buffer);

		return new OutboxFile(fileStream.Name, buffer.ToArray());
	}
}
