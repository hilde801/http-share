namespace HttpShare.Files;

public interface IInboxFile : IServerFile
{
	public string SenderName { get; }
}
