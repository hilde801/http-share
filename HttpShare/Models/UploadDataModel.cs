using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

namespace HttpShare.Models;

public sealed record UploadDataModel(
	ICollection<IFormFile> Files,
	string DisplayName
);
