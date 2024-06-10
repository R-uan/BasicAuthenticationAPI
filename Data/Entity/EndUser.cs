namespace BasicAuthenticationAPI;

public class EndUser {
	public int EndUserId { get; set; }
	public required string Username { get; set; }
	public required string Password { get; set; }
	public required string FirstName { get; set; }
	public required string LastName { get; set; }
}
