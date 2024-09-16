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


	public void AddServerEvent(ServerEventType type, string message)
	{
		ParsedDataContext.AddLog(type, message);
	}
}
