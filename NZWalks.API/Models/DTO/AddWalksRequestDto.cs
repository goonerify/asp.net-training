using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class AddWalksRequestDto
	{
		[Required]
		[MaxLength(60, ErrorMessage = "Name must me a max of 60 chars")]
		public required string Name { get; set; }

		[Required]
		[MaxLength(500, ErrorMessage = "Description must me a max of 500 chars")]
		public required string Description { get; set; }

		[Required]
		[Range(0, 100, ErrorMessage = "Length must be between 0 and 100 km")]
		public double LengthInKm { get; set; }

		public string? WalkImageUrl { get; set; }

		[Required]
		public Guid DifficultyId { get; set; }

		[Required]
		public Guid RegionId { get; set; }
	}
}

