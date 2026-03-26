using APIRestCofrinho;

namespace PrjCofrinho.Server.Classes
{
    // Classe com o objeto abstrato que será salvo nos dados
    public record Pessoas(int Id_Pessoa, String Nome, int Idade);

    // Interface com as funções que a tabela de Pessoas terá
    public interface IPessoas
    {
        #region Funções de CRUD básico da tabela
        // Criar
        Pessoas CreatePessoa(Pessoas pessoa);

        // Ler
        List<Pessoas> GetPessoas();
        Pessoas? GetPessoaById(int id);

        // Atualizar
        void UpdatePessoa(Pessoas pessoa);

        // Remover
        void DeletePessoaById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Pessoas
    public class PessoasService : IPessoas
    {
        /// <summary>
        /// Cria uma nova pessoa no servidor com base em um objeto
        /// </summary>
        /// <param name="pessoa">Nova pessoa salva no servidor.</param>
        /// <returns>Se bem sucedido, retorna a pessoa que foi adicionada, o id real dessa pessoa não é decidido por aqui e nem é devolvido aqui.</returns>
        public Pessoas CreatePessoa(Pessoas pessoa)
        {
            // Mandar criar a nova entrada na tabela
            string sqlQuery = $"INSERT INTO pessoas (nome, idade) VALUES ('{pessoa.Nome}', {pessoa.Idade});";

            // Enviar o pedido de criação
            ConexaoBD.executarQuery(sqlQuery, null);

            // Mostrar o item que foi adicionado
            return pessoa;
        }

        /// <summary>
        /// Remove uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da pessoa que será retirada da lista</param>
        public void DeletePessoaById(int id)
        {
            // Ao deletar uma pessoa, suas transações também devem ser deletadas
            string sqlQuery = $"DELETE FROM transacoes WHERE id_pessoa = {id};" +
                // Depois remover a pessoa da sua própria tabela
                $"DELETE FROM pessoas WHERE id_pessoa = {id};";

            // Enviar o pedido de remoção da pessoa
            ConexaoBD.executarQuery(sqlQuery, null);
        }

        /// <summary>
        /// Procura uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da pessoa que será buscada da lista</param>
        /// <returns>O objeto da pessoa salva ou um nulo, caso ela não esteja na lista</returns>
        public Pessoas? GetPessoaById(int id)
        {
            // Selecionar de pessoas só quem tem esse id
            string sqlQuery = $"SELECT * FROM pessoas WHERE id_pessoa = {id};";

            // Base onde talvez o objeto da pessoa seja criada, caso algo retorne, começa como nula
            Pessoas? pessoa = null;

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                if (result.Read())
                {
                    // Pegar os dados lidos
                    string nome = result.GetString(1);
                    int idade = result.GetInt32(2);

                    // Enviar um objeto com esses dados
                    pessoa = new Pessoas(id, nome, idade);
                }
            });
            
            // Retornar o que foi achado na busca
            return pessoa;
        }

        /// <summary>
        /// Volta toda a lista de pessoas salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Pessoas> GetPessoas()
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"SELECT * FROM pessoas;";

            // Lista que vai ter todas as entradas achadas 
            List<Pessoas> pessoas = [];

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                while (result.Read())
                {
                    // Pegar os dados lidos
                    int id = result.GetInt32(0);
                    string nome = result.GetString(1);
                    int idade = result.GetInt32(2);

                    // Enviar um objeto com esses dados
                    pessoas.Add(new Pessoas(id, nome, idade));
                }
            });

            // Simplesmente mostrar o conjunto todo
            return pessoas;
        }

        /// <summary>
        /// Procura uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="pessoa">Novo objeto pessoa que será salvo na lista</param>
        public void UpdatePessoa(Pessoas pessoa)
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"UPDATE pessoas SET Nome='{pessoa.Nome}', Idade={pessoa.Idade} WHERE Id_Pessoa = {pessoa.Id_Pessoa};";

            // Mandar o pedido de atualização
            ConexaoBD.executarQuery(sqlQuery, null);
        }
    }
}
