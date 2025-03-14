using MongoDB.Driver;
using AppGestaoDeResiduos.Models;

namespace AppGestaoDeResiduos.Data.Repositories
{
    public class ColetaRepository
    {
        private readonly IMongoCollection<Coleta> _coletas;

        public ColetaRepository(MongoDBContext context)
        {
            _coletas = context.GetCollection<Coleta>("Coletas");
        }

        public async Task AdicionarAsync(Coleta coleta)
        {
            await _coletas.InsertOneAsync(coleta);
        }

        public async Task<List<Coleta>> ObterTodosAsync()
        {
            return await _coletas.Find(_ => true).ToListAsync();
        }

        public async Task<Coleta> ObterPorIdAsync(string id)
        {
            return await _coletas.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AtualizarAsync(string id, Coleta coleta)
        {
            await _coletas.ReplaceOneAsync(c => c.Id == id, coleta);
        }

        public async Task RemoverAsync(string id)
        {
            await _coletas.DeleteOneAsync(c => c.Id == id);
        }
    }
}