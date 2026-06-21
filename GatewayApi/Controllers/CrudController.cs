using Business.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Application.Interfaces;

namespace GateWayApi.Controllers
{
    public abstract class CrudController<TEntity, TRequestDto, TResultDto> : ControllerBase
        where TEntity : class, IBaseEntity 
        where TRequestDto : class 
        where TResultDto : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected CrudController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(CancellationToken token)
        {
            var entities = await _repository.GetAllAsync(token);
            var resultList = entities.Select(e => MapToDto(e)).ToList();
            return Ok(resultList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken token)
        {
            var entity = await _repository.GetByIdAsync(id, token);

            if (entity == null) return NotFound();
            var resultDto = MapToDto(entity);
            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TRequestDto requestEntity, CancellationToken token)
        {
            var entity = MapToEntity(requestEntity);

            await _repository.AddAsync(entity, token);
            await _repository.SaveChangesAsync(token);

            return CreatedAtAction(nameof(GetById), new { id = entity.Guid }, MapToDto(entity));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TRequestDto requestEntity, CancellationToken token)
        {
            var entity = await _repository.GetByIdAsync(GetDtoId(requestEntity), token);

            if (entity == null) return NotFound();

            ApplyDtoToEntity(entity, requestEntity);

            _repository.Update(entity);
            await _repository.SaveChangesAsync(token);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken token)
        {
            var entity = await _repository.GetByIdAsync(id, token);

            if (entity == null)
            {
                return NotFound();
            }

            _repository.Delete(entity);
            await _repository.SaveChangesAsync(token);
            return NoContent();
        }

        protected abstract TEntity MapToEntity(TRequestDto dto);
        protected abstract TResultDto MapToDto(TEntity entity);
        protected abstract Guid GetDtoId(TRequestDto dto);
        protected abstract void ApplyDtoToEntity(TEntity entity, TRequestDto dto);

    }
}