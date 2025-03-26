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

		public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, 
			bool isAscending, int pageNumber, int pageSize)
		{
			// Include information for Difficulty and Region using thd difficultyId and regionId
			// Both Include calls are the same, but the first is type safe
			IQueryable<Walk> walks = context.Walks.Include(x => x.Difficulty).Include("Region");

			// filtering
			if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
			{

				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = walks.Where(x => x.Name.Contains(filterQuery));
				}
			}

			// sorting
			if (!string.IsNullOrWhiteSpace(sortBy))
			{
				if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) {
					walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
				} else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)) {
					walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
				}
			}

			// pagination
			var skipResults = (pageNumber - 1) * pageSize;

			return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
			//return await context.Walks.Include(x => x.Difficulty).Include("Region").ToListAsync();
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
