using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController(
		IRegionRepository regionRepository,
		IMapper mapper,
		ILogger<RegionsController> logger) : ControllerBase
	{
		private readonly IMapper mapper = mapper;

		// https://localhost:portnumber/api/regions
		[HttpGet]
		[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetAll()
		{
			// Get data from Database - Domain models
			var regions = await regionRepository.GetAllAsync();

			// Map Domain Models to DTOs
			var regionsDto = mapper.Map<List<RegionDto>>(regions);
			logger.LogInformation($"Regions retrieved: {JsonSerializer.Serialize(regionsDto)}");

			// Return DTOs
			return Ok(regionsDto);
		}

		// https://localhost:portnumber/api/regions
		[HttpGet]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			// NOTE: Find can only be used with the Id property
			// var region = dbContext.Regions.Find(id);
			// NOTE: Linq
			var region = await regionRepository.GetByIdAsync(id);

			if (region == null)
			{
				logger.LogWarning($"Region with id {id} not found");
				return NotFound("Foo does not exist");
			}

			//var regionsDto = new RegionDto()
			//{
			//	Id = region.Id,
			//	Code = region.Code,
			//	Name = region.Name,
			//	RegionImageUrl = region.RegionImageUrl
			//};

			var regionsDto = mapper.Map<RegionDto>(region);

			return Ok(regionsDto);
		}

		[HttpPost]
		[ValidateModel] // ValidateModelAttribute Custom Action Filter
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionDto)
		{
			//if(!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			//var region = new Region()
			//{
			//	Code = addRegionDto.Code,
			//	Name = addRegionDto.Name,
			//	RegionImageUrl = addRegionDto.RegionImageUrl
			//};

			var region = mapper.Map<Region>(addRegionDto);

			region = await regionRepository.CreateAsync(region);

			//var regionDto = new RegionDto()
			//{
			//	Id = region.Id,
			//	Code = region.Code,
			//	Name = region.Name,
			//	RegionImageUrl = region.RegionImageUrl
			//};

			var regionDto = mapper.Map<RegionDto>(region);

			return CreatedAtAction(nameof(GetById), new { id = region.Id, version = 5 }, regionDto);
		}

		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel] // ValidateModelAttribute Custom Action Filter
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromBody] UpdateRegionRequestDto updateRegionDto, [FromRoute] Guid id)
		{
			var region = await regionRepository.UpdateAsync(id, updateRegionDto);

			if (region == null)
			{
				logger.LogWarning($"Region with id {id} not found");
				return NotFound("Bar does not exist");
			}

			//var regionDto = new RegionDto
			//{
			//	Id = region.Id,
			//	Code = region.Code,
			//	Name = region.Name,
			//	RegionImageUrl = region.RegionImageUrl
			//};

			var regionDto = mapper.Map<RegionDto>(region);

			return Ok(regionDto);
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Writer, Reader")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var region = await regionRepository.DeleteAsync(id);

			if (region == null)
			{
				return NotFound("Bar does not exist");
			}

			return Ok(id);
		}
	}
}
