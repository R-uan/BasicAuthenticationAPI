namespace BasicAuthenticationAPI;

public interface IEndUserService {
	//
	//	Summary:
	//		Converts the EndUser to a EndUserDTO; Hides the password.
	//	Returns:
	//		EndUserDTO or null.
	Task<EndUserDTO?> FindByUsername(string username);
	//
	//	Summary:
	//		Converts the EndUserDTO to a EndUser and the oposite on the return.
	//	Returns:
	//		EndUserDTO
	Task<EndUserDTO> Save(EndUserDTO endUser);
	//
	//	Returns:
	//		List of EndUserDTO
	Task<List<EndUserDTO>> Find();
}
