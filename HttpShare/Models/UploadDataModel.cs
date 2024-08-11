// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

namespace HttpShare.Models;

public sealed record UploadDataModel(
	ICollection<IFormFile> Files,
	string DisplayName
);
