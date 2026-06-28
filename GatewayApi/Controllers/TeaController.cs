using Business.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Application.TeaMatchService;
using GateWayApi.Contracts.Tea;
using Service.Application.Interfaces;
using Business.Data.Enums;

namespace GateWayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeaController : CrudController<Tea, TeaRequestDto, TeaResponseDto>
    {
        private readonly TeaMatchService _teaMatchService;
        private readonly ILogger<TeaController> _logger;
        private readonly ITeaRepository _teaRepository;
        public TeaController(
                TeaMatchService teaMatchService,
                ILogger<TeaController> logger,
                ITeaRepository teaRepository) : base(teaRepository)
        {
            _teaMatchService = teaMatchService;
            _logger = logger;
            _teaRepository = teaRepository;
        }

        [HttpGet("tea-match")]
        public async Task<ActionResult> GetTeaMatch([FromQuery] Guid userId, CancellationToken token)
        {
            try
            {
                var result = await _teaMatchService.GetMatchingTeaAsync(userId, token);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting tea match");
                return StatusCode(500, "Internal server error");
            }
        }

        public override async Task<ActionResult> GetById(Guid id, CancellationToken token)
        {
            var tea = await _teaRepository.GetByIdWithIngredientsAsync(id, token);

            if (tea == null) return NotFound();

            return Ok(
                new TeaDetailsResponseDto
                {
                    TeaId = tea.Guid,
                    Name = tea.Name,
                    Description = tea.Description,
                    Image = tea.ImageUrl,
                    Ingredients = tea.TeaIngredients.Select(ti => new TeaIngredientResponseDto
                    {
                        IngredientId = ti.IngredientId,
                        Name = ti.Ingredient.Name,
                        Amount = ti.Amount
                    }).ToList()
                }
            );
        }

        protected override void ApplyDtoToEntity(Tea entity, TeaRequestDto dto)
        {
            entity.Name = dto.Name ?? entity.Name;
            entity.Description = dto.Description ?? entity.Description;
            entity.ImageUrl = dto.Image ?? entity.ImageUrl;
            entity.ModerationStatus = ModerationStatus.Pending;
        }

        protected override Guid GetDtoId(TeaRequestDto dto)
        {
            return dto.TeaId;
        }

        protected override TeaResponseDto MapToDto(Tea entity)
        {
            return new TeaResponseDto
            {
                TeaId = entity.Guid,
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.ImageUrl
            };
        }

        protected override Tea MapToEntity(TeaRequestDto dto)
        {
            return new Tea
            {
                Name = dto.Name ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                ImageUrl = dto.Image ?? string.Empty
            };
        }
    }
}