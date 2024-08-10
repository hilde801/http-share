using HttpShare.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HttpShare.Controls;

public partial class InboxControl : UserControl
{
	private InboxControlDataContext ParsedDataContext => (InboxControlDataContext)DataContext;


	public InboxControl()
	{
		InitializeComponent();
	}

	public void AddInboxFiles(Dispatcher dispatcher, ICollection<InboxFile> inboxFiles)
	{
		dispatcher.Invoke(() =>
		{
			foreach (InboxFile file in inboxFiles) ParsedDataContext.InboxFiles.Add(file);
		});
	}
}
