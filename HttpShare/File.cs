// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.IO;

namespace HttpShare;

/// <summary>
/// Represents a file.
/// </summary>
/// <param name="FilePath">The full path of the file.</param>
/// <param name="Data">The contents of the file.</param>
public record File(string FilePath, byte[] Data)
{
	/// <summary>
	/// The size of the file in bytes.
	/// </summary>
	public long Size => Data.LongLength;

	/// <summary>
	/// The filename of the file not including parent directories.
	/// </summary>
	public string Filename => Path.GetFileName(FilePath);

	/// <summary>
	/// The file extension.
	/// </summary>
	public string Extension => Path.GetExtension(FilePath);
}
