using MongoDB.Driver;
using AppGestaoDeResiduos.Models;

namespace AppGestaoDeResiduos.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(MongoDBContext context)
        {
            _usuarios = context.GetCollection<Usuario>("Usuarios");
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _usuarios.Find(_ => true).ToListAsync();
        }

        public async Task<Usuario> ObterPorIdAsync(string id)
        {
            return await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task AtualizarAsync(string id, Usuario usuario)
        {
            await _usuarios.ReplaceOneAsync(u => u.Id == id, usuario);
        }

        public async Task RemoverAsync(string id)
        {
            await _usuarios.DeleteOneAsync(u => u.Id == id);
        }
    }
}