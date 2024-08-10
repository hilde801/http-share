using System.IO;

namespace HttpShare;

public record File(string FilePath, byte[] Data)
{
	public long Size => Data.LongLength;

	public string Filename => Path.GetFileName(FilePath);

	public string Extension => Path.GetExtension(FilePath);
}
