namespace HttpShare;

public record InboxFile(
	string SenderName,
	string FilePath,
	byte[] Data) : File(FilePath, Data);
