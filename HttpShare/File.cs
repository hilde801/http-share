// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.IO;

namespace HttpShare;

public record File(string FilePath, byte[] Data)
{
	public long Size => Data.LongLength;

	public string Filename => Path.GetFileName(FilePath);

	public string Extension => Path.GetExtension(FilePath);
}
