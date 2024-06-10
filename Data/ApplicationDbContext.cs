using Microsoft.EntityFrameworkCore;

namespace BasicAuthenticationAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
	public DbSet<EndUser> EndUsers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<EndUser>(user => {
			user.ToTable("EndUsers");
			EndUser admin = new() {
				EndUserId = 1,
				Username = "admin",
				LastName = "Yrrej",
				FirstName = "Jerry",
				Password = BC.HashPassword("password"),
			};
			user.HasData(admin);
		});
	}
}