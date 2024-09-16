namespace HttpShare.Sessions;

public sealed record ServerEvent(ServerEventType Type, string Message, DateTime Timestamp)
{
	public string TimestampText => Timestamp.ToString();
}
