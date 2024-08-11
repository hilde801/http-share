// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

namespace HttpShare;

public record InboxFile(
	string SenderName,
	string FilePath,
	byte[] Data) : File(FilePath, Data);
