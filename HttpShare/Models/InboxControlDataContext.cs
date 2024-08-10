using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HttpShare.Models;

public sealed class InboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	private void InvokePropertyChangedEvent([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}


	public ObservableCollection<InboxFile> InboxFiles { get; } = [];
}
