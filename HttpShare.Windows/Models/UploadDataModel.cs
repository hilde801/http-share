// Copyright 2024 Hilde801 (https://github.com/hilde801)
// This file is a part of http-share

using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

namespace HttpShare.Models;

/// <summary>
/// A data model record class for client file uploads.
/// </summary>
/// <param name="Files">A collection of files to be sent to the host.</param>
/// <param name="DisplayName">The client's display name.</param>
public sealed record UploadDataModel(
	ICollection<IFormFile> Files,
	string DisplayName
);
