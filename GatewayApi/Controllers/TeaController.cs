using Microsoft.AspNetCore.Mvc;
using Service.Application.TeaMatchService;

namespace GateWayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeaController : ControllerBase
    {
        private readonly TeaMatchService _teaMatchService;
        private readonly ILogger<TeaController> _logger;
        public TeaController(TeaMatchService teaMatchService, ILogger<TeaController> logger)
        {
            _teaMatchService = teaMatchService;
            _logger = logger;
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
                _logger.LogError($"{e}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}