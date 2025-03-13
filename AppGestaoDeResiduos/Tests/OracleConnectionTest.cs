using System;
using Oracle.ManagedDataAccess.Client;

namespace AppGestaoDeResiduos.Tests
{
    public class OracleConnectionTest
    {
        private readonly string _connectionString;

        public OracleConnectionTest(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void TestConnection()
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexão bem-sucedida com o banco de dados Oracle.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados Oracle: {ex.Message}");
            }
        }
    }
}
