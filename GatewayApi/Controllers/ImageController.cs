using GateWayApi.Contracts.Images;
using Microsoft.AspNetCore.Mvc;

namespace GateWayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload-image")]
        public async Task<ActionResult> UploadImage(
            [FromForm] IFormFile image,
            [FromForm] ImageCategory imageCategory,
            CancellationToken token
        )
        {
            if (image.Length == 0 || image.Length > 5 * 1024 * 1024)
            {
                return BadRequest("Invalid image size");
            }

            var allowedExtensions = new []{ ".jpg", ".jpeg", ".png", ".webp" };
            var imageExt = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(imageExt))
            {
                return BadRequest("Unsupported image format");
            }

            var fileName = $"{Guid.NewGuid()}{imageExt}";
            var folderName = GetFolderName(imageCategory);

            if (string.IsNullOrEmpty(folderName)) 
            {
                return BadRequest("Invalid image category");
            }

            var dir = Path.Combine(
                _environment.ContentRootPath,
                "wwwroot",
                "uploads",
                folderName
            );
            Directory.CreateDirectory(dir);
            await using var stream = System.IO.File.Create(
                    Path.Combine(dir, fileName));
            await image.CopyToAsync(stream, token);

            return Ok ( new
                {
                    imageUrl = $"/uploads/{folderName}/{fileName}"
                }
            );
        }

        private string GetFolderName(ImageCategory imageCategory)
        {
            return imageCategory switch
            {
                ImageCategory.Tea => "teas",
                ImageCategory.Ingredient => "ingredients",
                _ => string.Empty,
            };
        }
    }
}