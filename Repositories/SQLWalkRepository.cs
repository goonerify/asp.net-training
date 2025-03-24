using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
	public class SQLWalkRepository(NZWalksDbContext context): IWalkRepository
	{
		public async Task<Walk> CreateAsync(Walk walk)
		{
			// Add the walk to the database
			await context.Walks.AddAsync(walk);
			await context.SaveChangesAsync();
			return walk;
		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var walk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);

			if (walk == null)
			{
				return null;
			}

			context.Remove(walk);
			await context.SaveChangesAsync();

			return walk;
		}

		public async Task<List<Walk>> GetAllAsync()
		{
			// Include information for Difficulty and Region using thd difficultyId and regionId
			// Both Include calls are the same, but the first is type safe
			return await context.Walks.Include(x => x.Difficulty).Include("Region").ToListAsync();
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			return await context.Walks.Include(x => x.Difficulty).Include("Region").FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Walk?> UpdateAsync(Guid id, UpdateWalkRequestDto updateWalkDto)
		{
			var walk = await context.Walks.FindAsync(id);

			if (walk == null)
			{
				return null;
			}

			walk.Name = updateWalkDto.Name;
			walk.Description = updateWalkDto.Description;
			walk.LengthInKm = updateWalkDto.LengthInKm;
			walk.WalkImageUrl = updateWalkDto.WalkImageUrl;
			walk.DifficultyId = updateWalkDto.DifficultyId;
			walk.RegionId = updateWalkDto.RegionId;

			await context.SaveChangesAsync();

			return walk;
		}
	}
}
