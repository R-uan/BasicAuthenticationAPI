namespace BasicAuthenticationAPI;

public interface IAuthService {
	//
	//	Summary:
	//		Uses the username to find the EndUser entity.
	//		If finds the EndUser, compares the passwords.
	//		If the password match, uses the EndUser to generate a JWT token.
	//	Returns:
	// 		JWT Token if successfully authenticated or null.
	Task<string?> Authenticate(string username, string password);
	Task<EndUserDTO> Register(EndUserDTO endUser);
}
