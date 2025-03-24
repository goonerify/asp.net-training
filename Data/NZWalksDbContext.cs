using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : DbContext(dbContextOptions)
	{
		public DbSet<Difficulty> Difficulties { get; set; }

		public DbSet<Region> Regions { get; set; }

		public DbSet<Walk> Walks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data for difficulties
			// Easy, medium, hard
			var difficulties = new List<Difficulty>
			{
				new() { Id = Guid.Parse("053c1043-6fb4-41b9-9d3c-26a498a1413d"), Name = "Easy" },
				new() { Id = Guid.Parse("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"), Name = "Medium" },
				new() { Id = Guid.Parse("9a3de88c-1a60-4c91-9461-af43cacd1005"), Name = "Hard" }
			};

			// Seed difficulties to the database
			modelBuilder.Entity<Difficulty>().HasData(difficulties);

			// Seed data for regions
			var regions = new List<Region>
			{
				new() {Id = Guid.Parse("f3b3b3b3-6fb4-41b9-9d3c-26a498a1413d"), Name = "Northland", Code = "", RegionImageUrl = null },
				new() {Id = Guid.Parse("b88cfd61-9477-4e1d-b84b-dadaa0184bdf"), Name = "Auckland", Code = "", RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg" },
				new() {Id = Guid.Parse("9a3de88c-1a60-4c91-9461-af43cacd1005"), Name = "Waikato", Code = "", RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg"},
				new() {Id = Guid.Parse("5db28a12-9b19-4046-b6ad-01dc5a63b7de"), Name = "Bay of Plenty", Code = "", RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg"},
				new() {Id = Guid.Parse("5025788f-9ab6-4778-bdc6-46623d1e8de5"), Name = "Gisborne", Code = "", RegionImageUrl = null},
				new() {Id = Guid.Parse("4bf96fb5-4419-404b-96b2-3b894d0ba2f5"), Name = "Hawke's Bay", Code = "", RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg"},
				new() {Id = Guid.Parse("fed42b58-fd6a-4dad-a57f-4cb452ba4a8f"), Name = "Taranaki", Code = "", RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg"},
				new() {Id = Guid.Parse("9c11f8b5-252f-4808-a2b3-726d1d26f311"), Name = "Manawatu-Wanganui", Code = "", RegionImageUrl = null},
				new() {Id = Guid.Parse("b92ebf56-42fc-412b-9732-247a1921165c"), Name = "Wellington", Code = "", RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpg"},
				new() {Id = Guid.Parse("1b8d43d6-21ca-40b2-9c20-85a4e8750475"), Name = "Tasman", Code = "", RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpg"},
				new() {Id = Guid.Parse("b39553be-de94-4d3b-a231-0ede8cf20a6e"), Name = "Nelson", Code = "", RegionImageUrl = null},
			};

			// Seed regions to the database
			modelBuilder.Entity<Region>().HasData(regions);
		}

	}
}
