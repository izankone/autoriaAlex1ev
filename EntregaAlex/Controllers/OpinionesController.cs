using Microsoft.AspNetCore.Mvc;
using EntregaAlex.Services;
using EntregaAlex.Dtos;

namespace EntregaAlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionesController : ControllerBase
    {
        private readonly IOpinionesService _service;

        public OpinionesController(IOpinionesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OpinionesResponseDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpinionesResponseDto>> GetById(int id)
        {
            var resultado = await _service.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<OpinionesResponseDto>> Create(OpinionesRequestDto request)
        {
            var resultado = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OpinionesRequestDto request)
        {
            var resultado = await _service.UpdateAsync(id, request);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrado = await _service.DeleteAsync(id);
            if (!borrado) return NotFound();
            return NoContent();
        }
    }
}