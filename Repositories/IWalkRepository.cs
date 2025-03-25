using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateAsync(Walk walk);
		Task<Walk?> DeleteAsync(Guid id);
		Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize);
		Task<Walk?> GetByIdAsync(Guid id);
		Task<Walk?> UpdateAsync(Guid id, UpdateWalkRequestDto walkDomainModel);
	}
}
