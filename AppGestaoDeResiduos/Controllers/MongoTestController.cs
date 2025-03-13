using AppGestaoDeResiduos.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppGestaoDeResiduos.Controllers
{
    [Route("api/testmongo")] /* Para testar acessar o ➡ http://localhost5096/api/testmongo */
    [ApiController]
    public class TestMongoController : ControllerBase
    {
        private readonly MongoDBContext _context;

        public TestMongoController(MongoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                // Acessa a coleção "TestCollection" (crie uma coleção no MongoDB para testes)
                var collection = _context.GetCollection<object>("TestCollection");

                // Insere um documento de teste
                var document = new { Message = "Conexão com MongoDB funcionando!" };
                await collection.InsertOneAsync(document);

                return Ok("Documento inserido com sucesso no MongoDB.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar ao MongoDB: {ex.Message}");
            }
        }
    }
}