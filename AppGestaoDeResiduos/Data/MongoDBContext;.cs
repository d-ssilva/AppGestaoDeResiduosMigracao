using MongoDB.Driver;  /* Gerenciador da conexão do MongoDB e acesso as coleções do BD */

namespace AppGestaoDeResiduos.Data 
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string connectionString, string databaseName)
        {
            // Cria o cliente MongoDB
            var client = new MongoClient(connectionString);

            // Acessa o banco de dados
            _database = client.GetDatabase(databaseName);
        }

        /* Retorna uma coleção do MongoDB para um tipo específico (T).
           Método acessa as coleções como Caminhoes, Rotas, etc. */
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}