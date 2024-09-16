using System.Collections.Generic;
using System.ComponentModel;

using HttpShare.Sessions;

namespace HttpShare.Windows.DataContexts;

public sealed class LogControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	private List<ServerEvent> appLog = [];

	public IEnumerable<ServerEvent> AppLog => appLog;


	public void AddLog(ServerEvent serverEvent) => appLog.Add(serverEvent);
}
