using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;

		public RegionsController(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		// https://localhost:portnumber/api/regions
		[HttpGet]
		public IActionResult GetAll() {
			// Get data from Database - Domain models
			var regions = dbContext.Regions.ToList();

			// Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();

			foreach (var region in regions) {
				regionsDto.Add(new RegionDto() {
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl
				});
			}

			// Return DTOs
			return Ok(regionsDto);
		}

		// https://localhost:portnumber/api/regions
		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id) {
			// NOTE: Find can only be used with the Id property
			// var region = dbContext.Regions.Find(id);
			// NOTE: Linq
			var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if (region == null) {
				return NotFound("Foo does not exist");
			}

			var regionsDto = new RegionDto()
			{
				Id = region.Id,
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl
			};

			return Ok(regionsDto);
		}

		[HttpPost]
		public IActionResult Create([FromBody] AddRegionDto addRegionDto) {
			try
			{
				var region = new Region()
				{
					Code = addRegionDto.Code,
					Name = addRegionDto.Name,
					RegionImageUrl = addRegionDto.RegionImageUrl
				};

				dbContext.Regions.Add(region);
				dbContext.SaveChanges();

				var regionDto = new RegionDto()
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl
				};

				return CreatedAtAction(nameof(GetById), new { id = region.Id, version = 5 }, regionDto);
			} catch {
				return StatusCode(StatusCodes.Status500InternalServerError, "Bombaclat server error");
			}
		}

		[HttpPut]
		[Route("{id:Guid}")]
		public IActionResult Update([FromBody] UpdateRegionDto updateRegionDto, [FromRoute] Guid id) {
			try
			{
				var region = dbContext.Regions.Find(id);

				if (region == null)
				{
					return NotFound("Bar does not exist");
				}

				region.Code = updateRegionDto.Code;
				region.Name = updateRegionDto.Name;
				region.RegionImageUrl = updateRegionDto.RegionImageUrl;

				dbContext.SaveChanges();

				var regionDto = new RegionDto
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl
				};

				return Ok(regionDto);
			} catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "My Server error");
			}
		}

		[HttpDelete]
		[Route("{id:Guid}")]
		public IActionResult Delete([FromRoute] Guid id) {
			try
			{
				var region = dbContext.Regions.Find(id);

				if (region == null)
				{
					return NotFound("Bar does not exist");
				}

				dbContext.Remove(region);
				dbContext.SaveChanges();

				return Ok(id);

			}
			catch {
				return StatusCode(StatusCodes.Status500InternalServerError, "Delete Server error");
			}
		}
	}
}
