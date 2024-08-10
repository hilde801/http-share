using System.Collections.Generic;

namespace HttpShare;

public interface ISendSession
{
	public ICollection<File> OutboxFiles { get; }
}
