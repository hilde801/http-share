using System.Collections.ObjectModel;

using HttpShare.Sessions;

namespace HttpShare.Windows.DataContexts;

public sealed class LogControlDataContext
{
	public ObservableCollection<ServerSessionEvent> AppLog { get; } = [
		new ServerSessionEvent(ServerEventType.Information, "Ready")
	];
}
