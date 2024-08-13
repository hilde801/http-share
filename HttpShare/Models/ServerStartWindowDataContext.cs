using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HttpShare.Models;

public sealed class ServerStartWindowDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;


	private string serverAddress = string.Empty;

	public string ServerAddress
	{
		get => serverAddress;

		set
		{
			serverAddress = value;
			InvokePropertyChangedEvent();
		}
	}


	private void InvokePropertyChangedEvent([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
