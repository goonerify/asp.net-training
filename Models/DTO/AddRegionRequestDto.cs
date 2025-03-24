using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class AddRegionRequestDto
	{
		[Required]
		[MinLength(3, ErrorMessage = "Code must me a min of 3 chars")]
		[MaxLength(3, ErrorMessage = "Code must me a max of 3 chars")]
		public required string Code { get; set; }

		[Required]
		[MaxLength(60, ErrorMessage = "Name must me a max of 60 chars")]
		public required string Name { get; set; }

		public string? RegionImageUrl { get; set; }
	}
}
