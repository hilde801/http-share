using System.ComponentModel.DataAnnotations;

namespace HttpShare.Models;

public sealed record LogInModel(
	[Required, DataType(DataType.Text)] string DisplayName
);
