using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/TestController")]
    [ApiController]
    public class OracleTestController : ControllerBase
    {
        private readonly string _connectionString;

        public OracleTestController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    return Ok("Conexão bem-sucedida com o banco de dados Oracle.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar: {ex.Message}");
            }
        }
    }
}
