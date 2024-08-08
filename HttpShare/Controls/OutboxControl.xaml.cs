using HttpShare.Models;
using System.Windows.Controls;

namespace HttpShare.Controls;

public partial class OutboxControl : UserControl
{
	private OutboxControlDataContext ParsedDataContext => (OutboxControlDataContext)DataContext;


	public OutboxControl()
	{
		InitializeComponent();
	}
}
