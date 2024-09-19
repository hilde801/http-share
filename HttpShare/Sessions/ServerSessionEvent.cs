namespace HttpShare.Sessions;

public sealed class ServerSessionEvent(ServerEventType type, string message)
{
	public DateTime Timestamp { get; } = DateTime.UtcNow;

	public string TimestampText => Timestamp.ToLocalTime().ToString();

	public ServerEventType Type => type;

	public string Message => message;
}
