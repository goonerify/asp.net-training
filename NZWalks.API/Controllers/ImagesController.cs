using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController(IImageRepository imageRepository) : ControllerBase
	{
		// POST: /api/images/upload
		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request) {
			try
			{
				ValidateFileUpload(request);

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				// Convert dto to domain model
				var imageDomainModel = new Image
				{
					File = request.File,
					FileExtension = Path.GetExtension(request.File.FileName),
					FileSizeInBytes = request.File.Length,
					FileName = request.FileName,
					FileDescription = request.FileDescription,
				};

				await imageRepository.UploadImageAsync(imageDomainModel);

				// Use repository to upload image
				return Ok();
			}
			catch
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Upload image server error");
			}
		}

		private void ValidateFileUpload(ImageUploadRequestDto request)
		{
			var allowedExtensions = new[] {".jpg", ".jpeg", ".png" };

			if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
			{
				ModelState.AddModelError("File", "Invalid file extension. Only .jpg, .jpeg, .png are allowed.");
			}

			if (request.File.Length > 10485760)
			{
				ModelState.AddModelError("File", "File size exceeds 10MB.");
			}
		}
	}
}
