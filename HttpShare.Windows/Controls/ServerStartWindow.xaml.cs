// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System;
using System.Linq;
using System.Net;
using System.Windows;

using HttpShare.Windows.Models;

namespace HttpShare.Windows.Controls;

/// <summary>
/// The code-behind class for the server start notification dialog.
/// </summary>
public partial class ServerStartWindow : Window
{
	/// <summary>
	/// The data context object parsed to <see cref="ServerStartWindowDataContext"/>.
	/// </summary>
	private ServerStartWindowDataContext ParsedDataContext => (ServerStartWindowDataContext) DataContext;


	/// <summary>
	/// The class constructor.
	/// </summary>
	public ServerStartWindow()
	{
		InitializeComponent();
	}


	/// <summary>
	/// Overrides the OnInitialized method.
	/// </summary>
	protected override void OnInitialized(EventArgs e)
	{
		ParsedDataContext.ServerAddress = GetServerAddress();

		base.OnInitialized(e);
	}


	/// <summary>
	/// Handles the "OK" button on click event.
	/// </summary>
	private void OnClickOkButton(object? sender, RoutedEventArgs _)
	{
		Close();
	}


	/// <summary>
	/// Handles the "Copy to Clipboard" button on click event.
	/// </summary>
	private void OnClickCopyToClipboardButton(object? sender, RoutedEventArgs _)
	{
		Clipboard.SetText(ParsedDataContext.ServerAddress);
	}


	/// <summary>
	/// Checks whether if an <see cref="IPAddress"/> is a local address or not.
	/// This function will check if the address starts with "192.168" in accordance to RFC1918.
	/// </summary>
	/// <param name="address">The <see cref="IPAddress"/> to check.</param>
	/// <returns>Returns true if the input is a local address.</returns>
	private static bool CheckIfLocalAddress(IPAddress address)
	{
		byte[] bytes = address.GetAddressBytes();
		return bytes[0] == 192 && bytes[1] == 168;
	}

	/// <summary>
	/// Gets all local <see cref="IPAddress"/>es.
	/// </summary>
	/// <returns>The URI string of the first local address fetched.</returns>
	public static string GetServerAddress()
	{
		IPAddress[] ipAddresses = Dns.GetHostAddresses(Dns.GetHostName())
			.Where(CheckIfLocalAddress)
			.ToArray();

		return $"http://{ipAddresses[0]}:80";
	}
}
