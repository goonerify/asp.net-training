﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
	public class Image
	{
		public Guid Id { get; set; }

		[NotMapped] // We don't want to store this in the database
		public required IFormFile File { get; set; }

		public required string FileName { get; set; }

		public required string FileExtension { get; set; }

		public string? FileDescription { get; set; }

		public long FileSizeInBytes { get; set; }

		public string? FilePath { get; set; }
	}
}
