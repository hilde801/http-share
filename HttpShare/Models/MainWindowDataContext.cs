using System.ComponentModel;

namespace HttpShare.Models;

public sealed class MainWindowDataContext : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	private bool isServerRunning = false;



	public bool IsServerRunning
	{
		get => isServerRunning;

		set
		{
			isServerRunning = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}

	public string ServerToggleButtonText => IsServerRunning ? "Stop Server" : "Start Server";

}
