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
        Pessoas? UpdatePessoa(Pessoas pessoa);

        // Remover
        void DeletePessoaById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Pessoas
    public class PessoasService : IPessoas
    {
        // Lista com todas as pessoas
        // TODO: salvar em um banco de dados, atualmente só salva na memória
        private readonly List<Pessoas> _pessoas = [];

        /// <summary>
        /// Cria uma nova pessoa no servidor com base em um objeto
        /// </summary>
        /// <param name="pessoa">Nova pessoa salva no servidor</param>
        /// <returns>Se bem sucedido, retorna a pessoa que foi adicionada.</returns>
        public Pessoas CreatePessoa(Pessoas pessoa)
        {
            // Colocar o novo item dentro da lista
            _pessoas.Add(pessoa);

            // Mostrar o item que foi adicionado
            return pessoa;
        }

        /// <summary>
        /// Remove uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da pessoa que será retirada da lista</param>
        public void DeletePessoaById(int id)
        {
            // Procurar de todos os itens da lista, e remover aquele onde o ID bate
            _pessoas.RemoveAll(pessoa => pessoa.Id_Pessoa == id);
        }

        /// <summary>
        /// Procura uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da pessoa que será buscada da lista</param>
        /// <returns>O objeto da pessoa salva ou um nulo, caso ela não esteja na lista</returns>
        public Pessoas? GetPessoaById(int id)
        {
            // Procurar de todos os itens da lista
            foreach (Pessoas pessoa in _pessoas)
            {
                // Se o ID for de alguém
                if (pessoa.Id_Pessoa == id)
                    // Enviar esse registro
                    return pessoa;
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }

        /// <summary>
        /// Volta toda a lista de pessoas salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Pessoas> GetPessoas()
        {
            // Simplesmente mostrar o conjunto todo
            return _pessoas;
        }

        /// <summary>
        /// Procura uma pessoa do servidor com base no ID da tabela
        /// </summary>
        /// <param name="pessoa">Novo objeto pessoa que será salvo na lista</param>
        /// <returns>O novo objeto da pessoa salva, caso ela esteja na lista. Ou um nulo, caso ela não esteja.</returns>
        public Pessoas? UpdatePessoa(Pessoas pessoa)
        {
            // Procurar de todos os itens da lista
            foreach (Pessoas p in _pessoas)
            {
                // Se o ID for de alguém
                if (p.Id_Pessoa == pessoa.Id_Pessoa)
                {
                    // TODO: com certeza tem um jeito melhor de fazer essa atualizar
                    // Remover o anterior
                    _pessoas.Remove(p);

                    // E adicionar o novo
                    _pessoas.Add(pessoa);

                    return p;
                }
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }
    }
}
