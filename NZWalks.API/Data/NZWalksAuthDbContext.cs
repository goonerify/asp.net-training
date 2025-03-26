using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
	public class NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : IdentityDbContext(options)
	{
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			string readerRoleId = "2c413f9a-6333-4312-8144-5722967708fe";
			string writerRoleId = "9adf18d0-925e-477f-9317-8903e220ee5c";
			string readerRoleName = "Reader";
			string writerRoleName = "Writer";

			var roles = new List<IdentityRole>
			{
				new() { Id = readerRoleId, Name = readerRoleName, NormalizedName = readerRoleName.ToUpper(), ConcurrencyStamp = readerRoleId },
				new() { Id = writerRoleId, Name = writerRoleName, NormalizedName = writerRoleName.ToUpper(), ConcurrencyStamp = writerRoleId }
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
