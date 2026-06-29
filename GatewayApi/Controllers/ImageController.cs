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
            [FromForm] ImageRequestForm requestForm,
            CancellationToken token
        )
        {
            if (requestForm.Image.Length == 0 || requestForm.Image.Length > 5 * 1024 * 1024)
            {
                return BadRequest("Invalid image size");
            }

            var allowedExtensions = new []{ ".jpg", ".jpeg", ".png", ".webp" };
            var imageExt = Path.GetExtension(requestForm.Image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(imageExt))
            {
                return BadRequest("Unsupported image format");
            }

            var fileName = $"{Guid.NewGuid()}{imageExt}";
            var folderName = GetFolderName(requestForm.ImageCategory);

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
            await requestForm.Image.CopyToAsync(stream, token);

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