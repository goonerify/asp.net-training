namespace NZWalks.API.Models.Domain
{
	public class Walk
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }

		// NOTE: Foreign keys - Seems the naming convention is to use the name of the related entity followed by Id
		public Guid DifficultyId { get; set; }
		public Guid RegionId { get; set; }


		// Navigation properties.
		// Used to establish relationships when migrations are run
		public required Difficulty Difficulty { get; set; }
		public required Region Region { get; set; }
	}
}