using MongoDB.Driver;
using System;

namespace AppGestaoDeResiduos.Tests
{
    public class MongoConnectionTest
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public MongoConnectionTest(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public void TestConnection()
        {
            try
            {
                // Cria o cliente MongoDB
                var client = new MongoClient(_connectionString);

                // Tenta acessar o banco de dados
                var database = client.GetDatabase(_databaseName);

                // Lista as coleções para verificar se a conexão está funcionando
                var collections = database.ListCollectionNames().ToList();

                Console.WriteLine("Conexão bem-sucedida com o banco de dados MongoDB.");
                Console.WriteLine($"Número de coleções no banco de dados: {collections.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados MongoDB: {ex.Message}");
            }
        }
    }
}