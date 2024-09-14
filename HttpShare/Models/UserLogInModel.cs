using System.ComponentModel.DataAnnotations;

namespace HttpShare.Models;

public sealed record UserLogInModel(
	[Required, DataType(DataType.Text)] string DisplayName
);
