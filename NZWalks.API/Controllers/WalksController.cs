using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	// api/walks
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController(
		IMapper mapper,
		IWalkRepository walkRepository,
		ILogger<RegionsController> logger) : ControllerBase
	{
		// Create a new walk
		// POST: /api/walks
		[HttpPost]
		[ValidateModel] // ValidateModelAttribute Custom Action Filter
		// [Authorize(Roles = "Writer")]
		public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksDto)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			// Map AddWalksDto to Walk domain model
			var walkDomainModel = mapper.Map<Walk>(addWalksDto);

			await walkRepository.CreateAsync(walkDomainModel);

			var walkDto = mapper.Map<WalkDto>(walkDomainModel);

			return Ok(walkDto);
		}

		// Get Walks
		// GET: /api/walks?filterOn=name&filterQuery=foo&sortBy=name&isAscending=true&pageSize=10&pageNumber=1
		[HttpGet]
		// [Authorize(Roles = "Reader")]
		// TODO: IMPORTANT: Define domain values for filterOn, sortBy that will be displayed in Swagger
		// https://medium.com/@niteshsinghal85/multiple-example-for-parameters-in-swagger-with-asp-net-core-c4f3aaf1ae9f
		// otherwise, investigate swaggergen to see if it can be done
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
			var walksDto = mapper.Map<List<WalkDto>>(walks);
			return Ok(walksDto);
		}

		[HttpGet]
		[Route("{id:Guid}")]
		// [Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIdAsync(id);
			if (walkDomainModel == null)
			{
				logger.LogWarning($"Walk with id {id} not found");
				return NotFound("Walk does not exist");
			}

			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel] // ValidateModelAttribute Custom Action Filter
		// [Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkDto)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			var walk = await walkRepository.UpdateAsync(id, updateWalkDto);

			if (walk == null)
			{
				logger.LogWarning($"Walk with id {id} not found");
				return NotFound("Walk does not exist");
			}

			var walkDto = mapper.Map<WalkDto>(walk);
			return Ok(walkDto);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		// [Authorize(Roles = "Writer")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var region = await walkRepository.DeleteAsync(id);

			if (region == null)
			{
				logger.LogWarning($"Walk with id {id} not found");
				return NotFound("Walk does not exist");
			}

			return Ok(id);
		}
	}
}
