namespace PrjCofrinho.Server.Classes
{
    // Classe com o objeto abstrato que será salvo nos dados
    public record Categorias(int Id_Categoria, String Descricao, int Finalidade);

    // Interface com as funções que a tabela de Categorias terá
    public interface ICategorias
    {
        #region Funções de CRUD básico da tabela
        // Criar
        Categorias CreateCategoria(Categorias categoria);

        // Ler
        List<Categorias> GetCategorias();
        Categorias? GetCategoriaById(int id);

        // Atualizar
        Categorias? UpdateCategoria(Categorias categoria);

        // Remover
        void DeleteCategoriaById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Categorias
    public class CategoriasService : ICategorias
    {
        // Lista com todas as categorias
        // TODO: salvar em um banco de dados, atualmente só salva na memória
        private readonly List<Categorias> _categorias = [];

        /// <summary>
        /// Cria uma nova categoria no servidor com base em um objeto
        /// </summary>
        /// <param name="categoria">Nova categoria salva no servidor</param>
        /// <returns>Se bem sucedido, retorna a categoria que foi adicionada.</returns>
        public Categorias CreateCategoria(Categorias categoria)
        {
            // Colocar o novo item dentro da lista
            _categorias.Add(categoria);

            // Mostrar o item que foi adicionado
            return categoria;
        }

        /// <summary>
        /// Remove uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da categoria que será retirada da lista</param>
        public void DeleteCategoriaById(int id)
        {
            // Procurar de todos os itens da lista, e remover aquele onde o ID bate
            _categorias.RemoveAll(categorias => categorias.Id_Categoria == id);
        }

        /// <summary>
        /// Procura uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da categoria que será buscada da lista</param>
        /// <returns>O objeto da categoria salva ou um nulo, caso ela não esteja na lista</returns>
        public Categorias? GetCategoriaById(int id)
        {
            // Procurar de todos os itens da lista
            foreach (Categorias categoria in _categorias)
            {
                // Se o ID for de alguém
                if (categoria.Id_Categoria == id)
                    // Enviar esse registro
                    return categoria;
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }

        /// <summary>
        /// Volta toda a lista de categorias salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Categorias> GetCategorias()
        {
            // Simplesmente mostrar o conjunto todo
            return _categorias;
        }

        /// <summary>
        /// Procura uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="categoria">Novo objeto categoria que será salvo na lista</param>
        /// <returns>O novo objeto da categoria salva, caso ela esteja na lista. Ou um nulo, caso ela não esteja.</returns>
        public Categorias? UpdateCategoria(Categorias categoria)
        {
            // Procurar de todos os itens da lista
            foreach (Categorias c in _categorias)
            {
                // Se o ID for de alguém
                if (c.Id_Categoria == categoria.Id_Categoria)
                {
                    // TODO: com certeza tem um jeito melhor de fazer essa atualizar
                    // Remover o anterior
                    _categorias.Remove(c);

                    // E adicionar o novo
                    _categorias.Add(categoria);

                    return c;
                }
            }

            // Se ninguém tiver esse ID, mandar um item nulo
            return null;
        }
    }
}
