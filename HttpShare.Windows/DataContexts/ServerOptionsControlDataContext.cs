using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using HttpShare.Windows.Controls;

namespace HttpShare.Windows.DataContexts;

public sealed class ServerOptionsControlDataContext : INotifyPropertyChanged, IDataErrorInfo, ServerOptionsControl.IServerOptions
{
	public event PropertyChangedEventHandler? PropertyChanged;


	private readonly ICollection<KeyValuePair<string, string>> errors = [];


	public ServerOptionsControlDataContext()
	{
		PropertyChanged += ValidateInputs;
	}


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


	private string password = string.Empty, passwordConfirm = string.Empty;

	private bool enablePassword = false;

	public bool EnablePassword
	{
		get => enablePassword;

		set
		{
			enablePassword = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}

	public string Password
	{
		get => password;

		set
		{
			password = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}

	public string PasswordConfirm
	{
		get => passwordConfirm;

		set
		{
			passwordConfirm = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
		}
	}


	private void ValidateInputs(object? sender, PropertyChangedEventArgs args)
	{
		if (!string.IsNullOrEmpty(args.PropertyName) || !string.IsNullOrWhiteSpace(args.PropertyName))
			return;

		errors.Clear();

		if (!int.TryParse(portInput, out port))
		{
			string errorMessage = "The input must be a number.";
			errors.Add(new KeyValuePair<string, string>(nameof(PortInput), errorMessage));
		}

		if (port is < 0 or > ushort.MaxValue)
		{
			string errorMessage = $"The input must be between 0 and {ushort.MaxValue}.";
			errors.Add(new KeyValuePair<string, string>(nameof(PortInput), errorMessage));
		}

		if (Password.Length < 6)
		{
			string errorMessage = "The password must be 6 characters or longer.";
			errors.Add(new KeyValuePair<string, string>(nameof(Password), errorMessage));
		}

		if (Password != PasswordConfirm)
		{
			string errorMessage = "Passwords does not match.";
			errors.Add(new KeyValuePair<string, string>(nameof(PasswordConfirm), errorMessage));
		}

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Errors)));
	}


	public record ErrorItem(string Input, string Message);

	public IEnumerable<ErrorItem> Errors => errors.Select(item => new ErrorItem(item.Key, item.Value));

	public bool HasErrors => errors.Count > 0;
}
