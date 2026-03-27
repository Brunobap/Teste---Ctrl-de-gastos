using APIRestCofrinho;

namespace PrjCofrinho.Server.Classes
{
    // Classe com o objeto abstrato que será salvo nos dados
    public record Transacoes(int Id_Transacao, String Descricao, float Valor, int Id_Categoria, int Id_Pessoa);

    // Interface com as funções que a tabela de Transacoes terá
    public interface ITransacoes
    {
        #region Funções de CRUD básico da tabela
        // Criar
        Transacoes CreateTransacao(Transacoes transacao);

        // Ler
        List<Transacoes> GetTransacoes();
        Transacoes? GetTransacaoById(int id);

        // Atualizar
        void UpdateTransacao(Transacoes transacao);

        // Remover
        void DeleteTransacaoById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Transacoes
    public class TransacoesService : ITransacoes
    {
        /// <summary>
        /// Cria uma nova transacao no servidor com base em um objeto
        /// </summary>
        /// <param name="transacao">Nova transação salva no servidor</param>
        /// <returns>Se bem sucedido, retorna a transação que foi adicionada.</returns>
        public Transacoes CreateTransacao(Transacoes transacao)
        {
            // Mandar criar a nova entrada na tabela
            string sqlQuery = $"INSERT INTO transacoes (descricao, valor, id_categoria, id_pessoa) VALUES" +
                $" ('{transacao.Descricao}', {transacao.Valor}, {transacao.Id_Categoria}, {transacao.Id_Pessoa});";

            // Enviar o pedido de criação
            ConexaoBD.executarQuery(sqlQuery, null);

            // Mostrar o item que foi adicionado
            return transacao;
        }

        /// <summary>
        /// Remove uma transação do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da transação que será retirada da lista</param>
        public void DeleteTransacaoById(int id)
        {
            // Remover a transação da sua própria tabela
            string sqlQuery = $"DELETE FROM transacoes WHERE id_transacao = {id};";

            // Enviar o pedido de remoção da pessoa
            ConexaoBD.executarQuery(sqlQuery, null);
        }

        /// <summary>
        /// Procura uma transacao do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da transação que será buscada da lista</param>
        /// <returns>O objeto da transação salva ou um nulo, caso ela não esteja na lista</returns>
        public Transacoes? GetTransacaoById(int id)
        {
            // Selecionar de pessoas só quem tem esse id
            string sqlQuery = $"SELECT * FROM transacoes WHERE id_transacao = {id};";

            // Base onde talvez o objeto da pessoa seja criada, caso algo retorne, começa como nula
            Transacoes? transacao = null;

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                if (result.Read())
                {
                    // Pegar os dados lidos
                    string descricao = result.GetString(1);
                    float valor = result.GetFloat(2);
                    int id_categoria = result.GetInt32(3);
                    int id_pessoa = result.GetInt32(4);

                    // Enviar um objeto com esses dados
                    transacao = new Transacoes(id, descricao, valor, id_categoria, id_pessoa);
                }
            });

            // Retornar o que foi achado na busca
            return transacao;
        }

        /// <summary>
        /// Volta toda a lista de transações salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Transacoes> GetTransacoes()
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"SELECT * FROM transacoes;";

            // Lista que vai ter todas as entradas achadas 
            List<Transacoes> transacoes = [];

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                while (result.Read())
                {
                    // Pegar os dados lidos
                    int id= result.GetInt32(0);
                    string descricao = result.GetString(1);
                    float valor = result.GetFloat(2);
                    int id_categoria = result.GetInt32(3);
                    int id_pessoa = result.GetInt32(4);

                    // Enviar um objeto com esses dados
                    transacoes.Add(new Transacoes(id, descricao, valor, id_categoria, id_pessoa));
                }
            });

            // Simplesmente mostrar o conjunto todo
            return transacoes;
        }

        /// <summary>
        /// Procura uma transação do servidor com base no ID da tabela
        /// </summary>
        /// <param name="transacao">Novo objeto transação que será salvo na lista</param>
        /// <returns>O novo objeto da transação salva, caso ela esteja na lista. Ou um nulo, caso ela não esteja.</returns>
        public void UpdateTransacao(Transacoes transacao)
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"UPDATE transacoes SET Descricao='{transacao.Descricao}', Valor={transacao.Valor}," +
                $"Id_Categoria={transacao.Id_Categoria}, Id_Pessoa={transacao.Id_Pessoa} WHERE Id_Pessoa = {transacao.Id_Transacao};";

            // Mandar o pedido de atualização
            ConexaoBD.executarQuery(sqlQuery, null);
        }
    }
}
