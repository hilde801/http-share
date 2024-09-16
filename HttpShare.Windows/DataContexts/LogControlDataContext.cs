using System.Collections.Generic;
using System.ComponentModel;

using HttpShare.Sessions;

namespace HttpShare.Windows.DataContexts;

public sealed class LogControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public record AppLogItem(ServerEventType Type, string Message);

	private List<AppLogItem> appLog = [];

	public IEnumerable<AppLogItem> AppLog => appLog;


	public void AddLog(ServerEventType type, string message)
	{
		appLog.Add(new AppLogItem(type, message));
	}
}
