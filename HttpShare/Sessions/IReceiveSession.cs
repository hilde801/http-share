// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare.Sessions;

/// <summary>
/// Describes a receive session. 
/// </summary>
public interface IReceiveSession
{
	/// <summary>
	/// Used as a delegate for <see cref="OnReceivedFiles"/>.
	/// </summary>
	/// <param name="files">A collection of <see cref="OutboxFile"/>s received from the client.</param>
	public delegate void ReceivedFilesHandler(ICollection<InboxFile> files);


	/// <summary>
	/// This event is invoked when the host receive files from client devices.
	/// </summary>
	public event ReceivedFilesHandler? OnReceivedFiles;


	/// <summary>
	/// Used for handling receive file events.
	/// </summary>
	/// <param name="files">A collection of <see cref="OutboxFile"/>s received from the client.</param>
	public void InvokeReceivedFilesEvent(ICollection<InboxFile> files);
}
