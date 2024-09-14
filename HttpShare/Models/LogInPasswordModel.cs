using System.ComponentModel.DataAnnotations;

namespace HttpShare.Models;

public sealed record LogInPasswordModel(
	[Required]
	[DataType(DataType.Text)]
	string DisplayName,

	[Required]
	[DataType(DataType.Password)]
	string Password
);
