using MySqlConnector;
using System.Runtime.CompilerServices;

namespace APIRestCofrinho
{
    public static class ConexaoBD
    {
        /// <summary>
        /// Os dados para achar o BD e autenticar nele
        /// </summary>
        private static readonly string paramsCon = "Server=localhost;Database=cofrinho;User Id=usercofrinho;Password=C0fr1nh0";

        /// <summary>
        /// Função para executar uma query no BD
        /// </summary>
        /// <param name="sqlQuery">String com a query a ser executada</param>
        /// <param name="processamento">Função callback que faz algo com os dados recebidos do BD</param>
        /// <returns>Um objeto com os dados lidos</returns>
        public static void executarQuery(string sqlQuery, Action<MySqlDataReader>? processamento)
        {
            // Criar um pedido de conexão com essas informações
            using MySqlConnection connection = new(paramsCon);

            // Abrir a conexão com o banco
            connection.Open();

            // Colocar a comando na conexão
            using MySqlCommand command = new(sqlQuery, connection);

            // Mandar para a sua execução
            using MySqlDataReader reader = command.ExecuteReader();

            // Ver se os dados recebidos da Query serão usados, se forem, rodar a função de processamento
            processamento?.Invoke(reader);
        }
        
    }
}
