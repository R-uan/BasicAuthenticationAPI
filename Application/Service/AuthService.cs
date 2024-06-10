namespace BasicAuthenticationAPI;

public class AuthService(IEndUserRepository enduserRepository, JWTHelper jwtHelper) : IAuthService {
	private readonly JWTHelper jwt = jwtHelper;
	private readonly IEndUserRepository _endUserRepository = enduserRepository;

	public async Task<string?> Authenticate(string username, string password) {
		var user = await _endUserRepository.FindByUsername(username);
		if (user == null) return null;
		bool authentication = BC.Verify(password, user.Password);
		if (authentication == false) throw new Exception("unauthorized");
		var token = jwt.Generate(user);
		return token;
	}

	public async Task<EndUserDTO> Register(EndUserDTO endUser) {
		var save = await _endUserRepository.Save(
			new EndUser() {
				FirstName = endUser.FirstName,
				LastName = endUser.LastName,
				Password = endUser.Password!,
				Username = endUser.Username
			});
		return new EndUserDTO() {
			FirstName = save.FirstName,
			LastName = save.LastName,
			Password = save.Password,
			Username = save.Username
		};
	}
}
