using System.Collections.ObjectModel;

namespace HttpShare.Models;

public sealed class OutboxControlDataContext
{
	public ObservableCollection<File> OutboxFiles { get; } = [];
}
