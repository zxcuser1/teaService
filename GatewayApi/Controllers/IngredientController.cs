using Business.Data.Models;
using GateWayApi.Contracts.Ingredient;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Interfaces;

namespace GateWayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : CrudController<Ingredient, IngredientRequestDto, IngredientResponseDto>
    {
        private readonly IWebHostEnvironment _environment;
        public IngredientController(IRepository<Ingredient> repository, IWebHostEnvironment environment) : base(repository)
        {
            _environment = environment;
        }
        protected override void ApplyDtoToEntity(Ingredient entity, IngredientRequestDto dto)
        {
            entity.ImageUrl = dto.ImageUrl ?? entity.ImageUrl;
            entity.Name = dto.Name ?? entity.Name;
            entity.IsModerated = false;
        }

        protected override Guid GetDtoId(IngredientRequestDto dto)
        {
            return dto.IngredientId;
        }

        protected override IngredientResponseDto MapToDto(Ingredient entity)
        {
            return new IngredientResponseDto
            {
                IngredientId = entity.Guid,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                SuggestedUserId = entity.SuggestedUserId
            };
        }

        protected override Ingredient MapToEntity(IngredientRequestDto dto)
        {
            return new Ingredient
            {
                Name = dto.Name  ?? string.Empty,
                ImageUrl = dto.ImageUrl ?? string.Empty,
                SuggestedUserId = dto.SuggestedUserId
            };
        }
        [HttpPost("upload-image")]
        public async Task<ActionResult> UploadImage(
            [FromForm] IFormFile image, 
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

            var dir = Path.Combine(
                _environment.ContentRootPath,
                "wwwroot",
                "uploads",
                "ingredients"
            );
            Directory.CreateDirectory(dir);
            await using var stream = System.IO.File.Create(
                    Path.Combine(dir, fileName));
            await image.CopyToAsync(stream, token);

            return Ok ( new
                {
                    imageUrl = $"/uploads/ingredients/{fileName}"
                }
            );
        }
    }
}
