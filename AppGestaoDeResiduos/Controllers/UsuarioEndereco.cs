using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppGestaoDeResiduos.Data;
using AppGestaoDeResiduos.Models;
using AppGestaoDeResiduos.DTOs;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Endereco)
                .ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);

            if (usuario == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuarioComEndereco(UsuarioEnderecoDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var endereco = new Endereco
                {
                    Cep = dto.Cep,
                    Estado = dto.Estado,
                    Cidade = dto.Cidade,
                    Rua = dto.Rua,
                    Numero = dto.Numero
                };

                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();

                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    AgendouColeta = dto.AgendouColeta,
                    FoiNotificado = dto.FoiNotificado,
                    EnderecoId = endereco.EnderecoId
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { Message = "Erro ao criar o usuário com endereço", Details = ex.Message });
            }
        }
    }
}
