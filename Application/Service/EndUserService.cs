
namespace BasicAuthenticationAPI;

public class EndUserService(IEndUserRepository repository) : IEndUserService {
	private readonly IEndUserRepository _repository = repository;

	public async Task<EndUserDTO?> FindByUsername(string username) {
		var user = await _repository.FindByUsername(username);
		return user == null ? null : new EndUserDTO() {
			Username = user.Username,
			Password = user.Password,
			LastName = user.LastName,
			FirstName = user.FirstName,
		};
	}

	public async Task<EndUserDTO> Save(EndUserDTO endUser) {
		var map = new EndUser() {
			Username = endUser.Username,
			FirstName = endUser.FirstName,
			LastName = endUser.LastName,
			Password = BC.HashPassword(endUser.Password),
		};

		var save = await _repository.Save(map);
		return new EndUserDTO() {
			Username = save.Username,
			Password = save.Password,
			LastName = save.LastName,
			FirstName = save.FirstName,
		};
	}

	public async Task<List<EndUserDTO>> Find() {
		return await _repository.Find();
	}
}
