using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HttpShare.Models;

public sealed class OutboxControlDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	private void InvokePropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}


	private readonly List<File> outboxFiles = [];


	public ICollection<File> OutboxFiles => outboxFiles;


	public void Add(File file)
	{
		outboxFiles.Add(file);
		InvokePropertyChanged(nameof(OutboxFiles));
	}

	public void Add(ICollection<File> files)
	{
		outboxFiles.AddRange(files);
		InvokePropertyChanged(nameof(OutboxFiles));
	}
}
