using System.ComponentModel.DataAnnotations;

namespace HttpShare.Models;

public sealed record LogIn(
	[Required, DataType(DataType.Text)] string DisplayName
);
