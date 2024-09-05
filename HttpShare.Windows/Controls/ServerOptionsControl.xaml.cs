// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Windows;
using System.Windows.Controls;

using HttpShare.Windows.DataContexts;

namespace HttpShare.Windows.Controls;

public partial class ServerOptionsControl : UserControl
{
	private ServerOptionsControlDataContext ParsedDataContext => (ServerOptionsControlDataContext) DataContext;


	public ServerOptionsControl()
	{
		InitializeComponent();
	}

	private void OnPasswordChangedPasswordInput(object sender, RoutedEventArgs e)
	{
		ParsedDataContext.Password = (sender as PasswordBox)!.Password;
	}

	private void OnPasswordChangedPasswordConfirmInput(object sender, RoutedEventArgs e)
	{
		ParsedDataContext.PasswordConfirm = (sender as PasswordBox)!.Password;
	}
}
