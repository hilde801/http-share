// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare;

/// <summary>
/// Represents a file sent from client devices.
/// </summary>
/// <param name="SenderName">The display name of the sender.</param>
/// <param name="FileName">The name of the file.</param>
/// <param name="Data">The contents of the file.</param>
public record InboxFile(
	string SenderName,
	string FileName,
	byte[] Data) : File(FileName, Data);
