using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
	public class SQLRegionRepository(NZWalksDbContext dbcontext) : IRegionRepository
	{
		private readonly NZWalksDbContext dbContext = dbcontext;

		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.Regions.AddAsync(region);

			await dbContext.SaveChangesAsync();

			return region;
		}

		public async Task<Region?> DeleteAsync(Guid id)
		{
			var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (region == null)
			{
				return null;
			}

			dbContext.Remove(region);
			await dbContext.SaveChangesAsync();

			return region;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			return await dbContext.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Region?> UpdateAsync(Guid id, UpdateRegionRequestDto updateRegionDto)
		{
			var region = await dbContext.Regions.FindAsync(id);

			if (region == null)
			{
				return null;
			}

			region.Code = updateRegionDto.Code;
			region.Name = updateRegionDto.Name;
			region.RegionImageUrl = updateRegionDto.RegionImageUrl;

			await dbContext.SaveChangesAsync();

			return region;
		}
	}
}
