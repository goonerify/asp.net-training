using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	// api/walks
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController(IMapper mapper, IWalkRepository walkRepository) : ControllerBase
	{
		// Create a new walk
		// POST: /api/walks
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalksDto addWalksDto)
		{
			try {
				// Map AddWalksDto to Walk domain model
				var walkDomainModel = mapper.Map<Walk>(addWalksDto);

				await walkRepository.CreateAsync(walkDomainModel);

				var walkDto = mapper.Map<WalkDto>(walkDomainModel);

				return Ok(walkDto);
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Create walk server error");
			}
		}

		// Get Walks
		// GET: /api/walks
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var walks = await walkRepository.GetAllAsync();
				var walksDto = mapper.Map<List<WalkDto>>(walks);
				return Ok(walksDto);
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Get all walks server error");
			}
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			try
			{
				var walkDomainModel = await walkRepository.GetByIdAsync(id);
				if (walkDomainModel == null)
				{
					return NotFound("Walk does not exist");
				}

				var walkDto = mapper.Map<WalkDto>(walkDomainModel);
				return Ok(walkDto);
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Get walk by id server error");
			}
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
		{
			try
			{
				var walk = await walkRepository.UpdateAsync(id, updateWalkDto);

				if (walk == null)
				{
					return NotFound("Walk does not exist");
				}
				
				var walkDto = mapper.Map<WalkDto>(walk);
				return Ok(walkDto);
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Update walk server error");
			}
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			try
			{
				var region = await walkRepository.DeleteAsync(id);

				if (region == null)
				{
					return NotFound("Walk does not exist");
				}

				return Ok(id);
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Delete walk server error");
			}
		}
	}
}
