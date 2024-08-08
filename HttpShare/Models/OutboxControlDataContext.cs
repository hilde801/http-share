using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HttpShare.Models;

public sealed class OutboxControlDataContext
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public ObservableCollection<File> OutboxFiles { get; } = [];
}
