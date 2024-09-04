// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare.Files;

/// <summary>
/// Represents a file sent from client devices.
/// </summary>
/// <param name="SenderName">The display name of the sender.</param>
/// <param name="FileName">The name of the file.</param>
/// <param name="Data">The contents of the file.</param>
internal record InboxFile(string SenderName, string FileName, byte[] Data) : IInboxFile
{
	/// <summary>
	/// The size of the file in bytes.
	/// </summary>
	public long Length => Data.LongLength;

	public string Name => throw new NotImplementedException();
}
