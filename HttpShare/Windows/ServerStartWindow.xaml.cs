using System;
using System.Linq;
using System.Net;
using System.Windows;

using HttpShare.Models;

namespace HttpShare.Windows;

public partial class ServerStartWindow : Window
{
	private ServerStartWindowDataContext ParsedDataContext => (ServerStartWindowDataContext) DataContext;


	public ServerStartWindow()
	{
		InitializeComponent();
	}


	protected override void OnInitialized(EventArgs e)
	{
		ParsedDataContext.ServerAddress = GetServerAddress();

		base.OnInitialized(e);
	}


	private static bool CheckIfLocalAddress(IPAddress address)
	{
		byte[] bytes = address.GetAddressBytes();
		return bytes[0] == 192 && bytes[1] == 168;
	}

	public static string GetServerAddress()
	{
		IPAddress[] ipAddresses = Dns.GetHostAddresses(Dns.GetHostName())
			.Where(CheckIfLocalAddress)
			.ToArray();

		return $"http://{ipAddresses[0]}:80";
	}
}
