using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasicAuthenticationAPI;

public class EndUserDTO {
	public required string Username { get; set; }
	[JsonIgnore]
	public string? Password { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
}
