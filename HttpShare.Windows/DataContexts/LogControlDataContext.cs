using System.Collections.Generic;
using System.ComponentModel;

using HttpShare.Sessions;

namespace HttpShare.Windows.DataContexts;

public sealed class LogControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public record AppLogItem(ServerEventType Type, string Message);

	private IEnumerable<AppLogItem> appLog = [];

	public IEnumerable<AppLogItem> AppLog => appLog;

}
