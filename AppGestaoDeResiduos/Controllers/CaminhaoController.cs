using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppGestaoDeResiduos.Data.Repositories;
using AppGestaoDeResiduos.Models;
using AppGestaoDeResiduos.Dto;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/Caminhoes")]
    [ApiController]
    public class CaminhaoController : ControllerBase
    {
        private readonly CaminhaoRepository _caminhaoRepository;
        private readonly ColetaRepository _coletaRepository; // Injetar o repositório de Coleta

        public CaminhaoController(CaminhaoRepository caminhaoRepository, ColetaRepository coletaRepository)
        {
            _caminhaoRepository = caminhaoRepository;
            _coletaRepository = coletaRepository; // Inicializar o repositório de Coleta
        }

        // POST: api/Caminhoes/Schedule
        [HttpPost("Schedule")]
        [Authorize]
        public async Task<ActionResult> ScheduleColeta([FromBody] Coleta coleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Adicionar a coleta usando o repositório
            await _coletaRepository.AdicionarAsync(coleta);

            return CreatedAtAction(nameof(GetColeta), new { id = coleta.Id }, coleta);
        }

        // GET: api/Caminhoes/Coletas/12345
        [HttpGet("Coletas/{id}")]
        public async Task<ActionResult<Coleta>> GetColeta(string id)
        {
            var coleta = await _coletaRepository.ObterPorIdAsync(id);
            if (coleta == null)
            {
                return NotFound();
            }
            return Ok(coleta);
        }
    }
}