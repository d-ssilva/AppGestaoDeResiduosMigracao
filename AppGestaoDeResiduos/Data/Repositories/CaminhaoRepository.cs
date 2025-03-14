using MongoDB.Driver;
using AppGestaoDeResiduos.Models;

namespace AppGestaoDeResiduos.Data.Repositories
{
    public class CaminhaoRepository
    {
        private readonly IMongoCollection<Caminhao> _caminhoes;

        public CaminhaoRepository(MongoDBContext context)
        {
            _caminhoes = context.GetCollection<Caminhao>("Caminhoes");
        }

        // Adicionar um novo caminhão
        public async Task AdicionarAsync(Caminhao caminhao)
        {
            await _caminhoes.InsertOneAsync(caminhao);
        }

        // Obter todos os caminhões
        public async Task<List<Caminhao>> ObterTodosAsync()
        {
            return await _caminhoes.Find(_ => true).ToListAsync();
        }

        // Obter um caminhão por ID
        public async Task<Caminhao> ObterPorIdAsync(string id)
        {
            return await _caminhoes.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        // Atualizar um caminhão
        public async Task AtualizarAsync(string id, Caminhao caminhao)
        {
            await _caminhoes.ReplaceOneAsync(c => c.Id == id, caminhao);
        }

        // Remover um caminhão
        public async Task RemoverAsync(string id)
        {
            await _caminhoes.DeleteOneAsync(c => c.Id == id);
        }
    }
}