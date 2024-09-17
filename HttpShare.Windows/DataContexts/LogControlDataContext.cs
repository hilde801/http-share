using System;
using System.Collections.ObjectModel;

using HttpShare.Sessions;

namespace HttpShare.Windows.DataContexts;

public sealed class LogControlDataContext
{
	public ObservableCollection<ServerEvent> AppLog { get; } = [
		new ServerEvent(ServerEventType.Information, "Ready", DateTime.UtcNow)
	];
}
