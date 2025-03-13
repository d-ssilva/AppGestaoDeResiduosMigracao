using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppGestaoDeResiduos.Data;
using AppGestaoDeResiduos.Models;
using AppGestaoDeResiduos.Dto;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/Caminhoes")]
    [ApiController]
    public class CaminhaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CaminhaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caminhao>>> GetAllCaminhoes(int pageNumber = 1, int pageSize = 10)
        {
            // Implementação de paginação
            var caminhoes = await _context.Caminhoes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(caminhoes);
        }

        [HttpGet("Track")]
        public async Task<ActionResult<IEnumerable<Caminhao>>> TrackCaminhoes()
        {
            var caminhoes = await _context.Caminhoes
                .Include(c => c.Localizacao)
                .ToListAsync();

            return Ok(caminhoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Caminhao>> GetCaminhao(int id)
        {
            var caminhao = await _context.Caminhoes
                .Include(c => c.Localizacao)
                .FirstOrDefaultAsync(c => c.CaminhaoId == id);

            if (caminhao == null)
            {
                return NotFound();
            }

            return Ok(caminhao);
        }

        [HttpPost("Schedule")]
        [Authorize]
        public async Task<ActionResult> ScheduleColeta([FromBody] Coleta coleta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Coletas.Add(coleta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColeta), new { id = coleta.ColetaId }, coleta);
        }

        [HttpGet("Coletas")]
        public async Task<ActionResult<IEnumerable<Coleta>>> GetColetas(int pageNumber = 1, int pageSize = 10)
        {
            var coletas = await _context.Coletas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(coletas);
        }

        [HttpGet("Coletas/{id}")]
        public async Task<ActionResult<Coleta>> GetColeta(int id)
        {
            var coleta = await _context.Coletas.FindAsync(id);

            if (coleta == null)
            {
                return NotFound();
            }

            return Ok(coleta);
        }

        [HttpPost]
        public async Task<ActionResult<Caminhao>> AddCaminhao([FromBody] Caminhao caminhao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localizacaoExistente = await _context.Localizacoes
                .FirstOrDefaultAsync(l => l.LocalizacaoId == caminhao.LocalizacaoId);

            if (localizacaoExistente == null)
            {
                var novaLocalizacao = new Localizacao
                {
                    LocalizacaoId = caminhao.LocalizacaoId.GetValueOrDefault(),
                    Longitude = 1,
                    Latitude = 2,
                    DataHora = DateTime.Now
                };

                _context.Localizacoes.Add(novaLocalizacao);
                await _context.SaveChangesAsync();
            }

            _context.Caminhoes.Add(caminhao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCaminhao), new { id = caminhao.CaminhaoId }, caminhao);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<Caminhao>> AddOrUpdateCaminhao([FromBody] Caminhao caminhao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localizacaoExistente = await _context.Localizacoes
                .FirstOrDefaultAsync(l => l.LocalizacaoId == caminhao.LocalizacaoId);

            if (localizacaoExistente == null)
            {
                var novaLocalizacao = new Localizacao
                {
                    LocalizacaoId = caminhao.LocalizacaoId.GetValueOrDefault(),
                    Longitude = 1,
                    Latitude = 2,
                    DataHora = DateTime.Now
                };

                _context.Localizacoes.Add(novaLocalizacao);
                await _context.SaveChangesAsync();
            }

            var caminhaoExistente = await _context.Caminhoes
                .FirstOrDefaultAsync(c => c.CaminhaoId == caminhao.CaminhaoId);

            if (caminhaoExistente != null)
            {
                caminhaoExistente.Placa = caminhao.Placa;
                caminhaoExistente.QtdDeColetas = caminhao.QtdDeColetas;
                caminhaoExistente.QtdDeColetasMax = caminhao.QtdDeColetasMax;
                caminhaoExistente.LocalizacaoId = caminhao.LocalizacaoId;

                _context.Caminhoes.Update(caminhaoExistente);
            }
            else
            {
                _context.Caminhoes.Add(caminhao);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCaminhao), new { id = caminhao.CaminhaoId }, caminhao);
        }

        [HttpPost("NotificarUsuario")]
        public async Task<ActionResult> PostNotificarUsuario([FromBody] NotificarUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var notificacao = new Notificacao
            {
                Mensagem = request.Mensagem
            };

            _context.Notificacoes.Add(notificacao);
            await _context.SaveChangesAsync();

            var usuarioNotificacao = new UsuarioNotificacao
            {
                UsuarioId = request.UsuarioId,
                NotificacaoId = (int)notificacao.NotificacaoId
            };

            _context.UsuarioNotificacoes.Add(usuarioNotificacao);

            usuario.FoiNotificado = true;
            _context.Usuarios.Update(usuario);

            await _context.SaveChangesAsync();

            return Ok("Usuário notificado com sucesso.");
        }
    }
}
