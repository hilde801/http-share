using System.Collections.Generic;

namespace HttpShare;

public sealed class DualSession(ICollection<File> outboxFiles) : ServerSession, ISendSession
{
	public ICollection<File> OutboxFiles => outboxFiles;
}
