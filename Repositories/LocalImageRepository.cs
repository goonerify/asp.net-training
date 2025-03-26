using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class LocalImageRepository(IWebHostEnvironment webHostEnvironment, 
		IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext) : IImageRepository
	{
		public async Task<Image> UploadImageAsync(Image image)
		{
			var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

			// Upload the image to the local file system
			using var stream = new FileStream(localFilePath, FileMode.Create);
			await image.File.CopyToAsync(stream);

			// E.g https://localhost:5001/Images/image.jpg
			var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" + 
				$"{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" + 
				$"/Images/{image.FileName}{image.FileExtension}";

			image.FilePath = urlFilePath;

			// Add the url file path to the database
			await dbContext.Images.AddAsync(image);
			await dbContext.SaveChangesAsync();

			return image;
		}
	}
}
