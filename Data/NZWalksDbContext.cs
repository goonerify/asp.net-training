using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : DbContext(dbContextOptions)
	{
		public DbSet<Difficulty> Difficulties { get; set; }

		public DbSet<Region> Regions { get; set; }

		public DbSet<Walk> Walks { get; set; }
	}
}
