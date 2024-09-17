namespace HttpShare.Sessions;

public sealed class ServerEvent(ServerEventType type, string message)
{
	public DateTime Timestamp { get; } = DateTime.UtcNow;

	public string TimestampText => Timestamp.ToLocalTime().ToString();

	public ServerEventType Type => type;

	public string Message => message;
}
