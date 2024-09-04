namespace HttpShare.Files;

public interface IServerFile
{
	public string Name { get; }

	public byte[] Data { get; }

	public long Length { get; }
}
