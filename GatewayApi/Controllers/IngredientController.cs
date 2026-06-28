using Business.Data.Enums;
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
        public IngredientController(IRepository<Ingredient> repository) : base(repository) { }
        protected override void ApplyDtoToEntity(Ingredient entity, IngredientRequestDto dto)
        {
            entity.ImageUrl = dto.ImageUrl ?? entity.ImageUrl;
            entity.Name = dto.Name ?? entity.Name;
            entity.ModerationStatus = ModerationStatus.Pending;
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
    }
}
