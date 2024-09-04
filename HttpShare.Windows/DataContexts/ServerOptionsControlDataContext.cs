using System.ComponentModel;

namespace HttpShare.Windows.DataContexts;

public sealed class ServerOptionsControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
}
