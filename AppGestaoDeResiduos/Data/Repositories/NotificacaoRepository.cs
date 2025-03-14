using MongoDB.Driver;
using AppGestaoDeResiduos.Models;

namespace AppGestaoDeResiduos.Data.Repositories
{
    public class NotificacaoRepository
    {
        private readonly IMongoCollection<Notificacao> _notificacoes;

        public NotificacaoRepository(MongoDBContext context)
        {
            _notificacoes = context.GetCollection<Notificacao>("Notificacoes");
        }

        // Adicionar uma nova notificação
        public async Task AdicionarAsync(Notificacao notificacao)
        {
            await _notificacoes.InsertOneAsync(notificacao);
        }

        // Obter todas as notificações
        public async Task<List<Notificacao>> ObterTodosAsync()
        {
            return await _notificacoes.Find(_ => true).ToListAsync();
        }

        // Obter uma notificação por ID
        public async Task<Notificacao> ObterPorIdAsync(string id)
        {
            return await _notificacoes.Find(n => n.Id == id).FirstOrDefaultAsync();
        }

        // Atualizar uma notificação
        public async Task AtualizarAsync(string id, Notificacao notificacao)
        {
            await _notificacoes.ReplaceOneAsync(n => n.Id == id, notificacao);
        }

        // Remover uma notificação
        public async Task RemoverAsync(string id)
        {
            await _notificacoes.DeleteOneAsync(n => n.Id == id);
        }
    }
}