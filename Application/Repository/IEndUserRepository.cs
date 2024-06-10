namespace BasicAuthenticationAPI;

public interface IEndUserRepository {
	//
	//	Summary:
	//		Finds the EndUser associated with the given username.
	//	Returns
	//		EndUser Entity or null if not found.
	Task<EndUser?> FindByUsername(string username);
	//
	//	Summary:
	//		Saves a EndUser in the database.
	//	Return:
	//		Returns the saved entity.
	Task<EndUser> Save(EndUser endUser);
	//
	//	Summary:
	//		Selects all the EndUser entities as EndUserDTO; Hides the password.
	//	Returns
	//		List of EndUserDTO.
	Task<List<EndUserDTO>> Find();
}
