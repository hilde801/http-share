using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HttpShare.Windows.DataContexts;

public sealed class ServerOptionsControlDataContext : INotifyPropertyChanged, IDataErrorInfo
{
	public event PropertyChangedEventHandler? PropertyChanged;


	public string Error => errors.Count == 0 ? string.Empty : errors.First().Value;

	public string this[string columnName]
	{
		get => errors.Where(pair => pair.Key == columnName)
			.Select(pair => pair.Value).FirstOrDefault(string.Empty);
	}


	private string portInput = "80";

	private int port = 80;

	public int Port => port;

	public string PortInput
	{
		get => portInput;

		set
		{
			portInput = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}
}
