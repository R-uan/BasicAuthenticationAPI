using BasicAuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthenticationAPI;

public class EndUserRepository(ApplicationDbContext dbContext) : IEndUserRepository {
	private readonly ApplicationDbContext db = dbContext;

	public async Task<List<EndUserDTO>> Find() {
		return await db.EndUsers.Select(user => new EndUserDTO() {
			FirstName = user.FirstName,
			LastName = user.LastName,
			Username = user.Username
		}).ToListAsync();
	}

	public async Task<EndUser?> FindByUsername(string username) {
		var user = await db.EndUsers
		.Where(user => user.Username == username)
		.FirstOrDefaultAsync();
		return user;
	}

	public async Task<EndUser> Save(EndUser endUser) {
		var save = await db.EndUsers.AddAsync(endUser);
		await db.SaveChangesAsync();
		return save.Entity ?? throw new Exception("Failed to save EndUser.");
	}
}
