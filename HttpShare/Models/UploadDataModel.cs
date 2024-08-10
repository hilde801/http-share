using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HttpShare.Models;

public sealed record UploadDataModel(
	ICollection<IFormFile> Files,
	string DisplayName
);
