using System.Windows.Controls;

using HttpShare.Sessions;
using HttpShare.Windows.DataContexts;

namespace HttpShare.Windows.Controls;

public partial class LogControl : UserControl
{
	private LogControlDataContext ParsedDataContext => (LogControlDataContext) DataContext!;


	public LogControl()
	{
		InitializeComponent();
	}


	public void AddServerEvent(ServerEvent serverEvent)
	{
		ParsedDataContext.AppLog.Add(serverEvent);
	}
}
