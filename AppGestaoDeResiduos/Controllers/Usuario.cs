using Microsoft.AspNetCore.Mvc;
using AppGestaoDeResiduos.Data.Repositories;
using AppGestaoDeResiduos.Models;
using System.Threading.Tasks;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public UsuariosController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var usuarios = await _repository.ObterTodosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {
            var usuario = await _repository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Usuario usuario)
        {
            await _repository.AdicionarAsync(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(string id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            await _repository.AtualizarAsync(id, usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(string id)
        {
            await _repository.RemoverAsync(id);
            return NoContent();
        }
    }
}