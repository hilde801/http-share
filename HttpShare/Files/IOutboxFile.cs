namespace HttpShare.Files;

public interface IOutboxFile : IServerFile
{
	public string FilePath { get; }
}
