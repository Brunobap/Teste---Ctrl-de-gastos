namespace PrjCofrinho.Server.Classes
{
    // Classe com o objeto abstrato que será salvo nos dados
    public record Transacoes(int Id_Transacao, String Descricao, int Valor, int Id_Categoria, int Id_Pessoa);

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
        Transacoes? UpdateTransacao(Transacoes transacao);

        // Remover
        void DeleteTransacaoById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Transacoes
    public class TransacoesService : ITransacoes
    {
        // Lista com todas as transacoes
        // TODO: salvar em um banco de dados, atualmente só salva na memória
        private readonly List<Transacoes> _transacoes = [];

        /// <summary>
        /// Cria uma nova transacao no servidor com base em um objeto
        /// </summary>
        /// <param name="transacao">Nova transação salva no servidor</param>
        /// <returns>Se bem sucedido, retorna a transação que foi adicionada.</returns>
        public Transacoes CreateTransacao(Transacoes transacao)
        {
            // Colocar o novo item dentro da lista
            _transacoes.Add(transacao);

            // Mostrar o item que foi adicionado
            return transacao;
        }

        /// <summary>
        /// Remove uma transação do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da transação que será retirada da lista</param>
        public void DeleteTransacaoById(int id)
        {
            // Procurar de todos os itens da lista, e remover aquele onde o ID bate
            _transacoes.RemoveAll(transacoes => transacoes.Id_Transacao == id);
        }

        /// <summary>
        /// Procura uma transacao do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da transação que será buscada da lista</param>
        /// <returns>O objeto da transação salva ou um nulo, caso ela não esteja na lista</returns>
        public Transacoes? GetTransacaoById(int id)
        {
            // Procurar de todos os itens da lista
            foreach (Transacoes transacao in _transacoes)
            {
                // Se o ID for de alguém
                if (transacao.Id_Transacao == id)
                    // Enviar esse registro
                    return transacao;
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }

        /// <summary>
        /// Volta toda a lista de transações salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Transacoes> GetTransacoes()
        {
            // Simplesmente mostrar o conjunto todo
            return _transacoes;
        }

        /// <summary>
        /// Procura uma transação do servidor com base no ID da tabela
        /// </summary>
        /// <param name="transacao">Novo objeto transação que será salvo na lista</param>
        /// <returns>O novo objeto da transação salva, caso ela esteja na lista. Ou um nulo, caso ela não esteja.</returns>
        public Transacoes? UpdateTransacao(Transacoes transacao)
        {
            // Procurar de todos os itens da lista
            foreach (Transacoes c in _transacoes)
            {
                // Se o ID for de alguém
                if (c.Id_Transacao == transacao.Id_Transacao)
                {
                    // TODO: com certeza tem um jeito melhor de fazer essa atualizar
                    // Remover o anterior
                    _transacoes.Remove(c);

                    // E adicionar o novo
                    _transacoes.Add(transacao);

                    return c;
                }
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }
    }
}
