// =============================================================================
// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share
// =============================================================================

using System.Collections.Generic;

namespace HttpShare;

public interface ISendSession
{
	public ICollection<File> OutboxFiles { get; }
}
