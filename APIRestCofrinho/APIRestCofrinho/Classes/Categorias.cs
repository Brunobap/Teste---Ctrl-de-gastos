using APIRestCofrinho;

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
        void UpdateCategoria(Categorias categoria);

        // Remover
        void DeleteCategoriaById(int id);
        #endregion
    }

    // Funções que serão executadas sobre a tabela Categorias
    public class CategoriasService : ICategorias
    {
        /// <summary>
        /// Cria uma nova categoria no servidor com base em um objeto
        /// </summary>
        /// <param name="categoria">Nova categoria salva no servidor</param>
        /// <returns>Se bem sucedido, retorna a categoria que foi adicionada, o id real dessa categoria não é decidido por aqui e nem é devolvido aqui.</returns>
        public Categorias CreateCategoria(Categorias categoria)
        {
            // Mandar criar a nova entrada na tabela
            string sqlQuery = $"INSERT INTO categorias (descricao, finalidade) VALUES ('{categoria.Descricao}', {categoria.Finalidade});";

            // Enviar o pedido de criação
            ConexaoBD.executarQuery(sqlQuery, null);

            // Mostrar o item que foi adicionado
            return categoria;
        }

        /// <summary>
        /// Remove uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da categoria que será retirada da lista</param>
        public void DeleteCategoriaById(int id)
        {
            // Ao deletar uma categoria, suas transações também devem ser deletadas
            string sqlQuery = $"DELETE FROM transacoes WHERE id_categoria = {id};" +
                // Depois remover a categoria da sua própria tabela
                $"DELETE FROM categorias WHERE id_categoria = {id};";

            // Enviar o pedido de remoção da pessoa
            ConexaoBD.executarQuery(sqlQuery, null);
        }

        /// <summary>
        /// Procura uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="id">ID da categoria que será buscada da lista</param>
        /// <returns>O objeto da categoria salva ou um nulo, caso ela não esteja na lista</returns>
        public Categorias? GetCategoriaById(int id)
        {
            // Selecionar de pessoas só quem tem esse id
            string sqlQuery = $"SELECT * FROM categorias WHERE id_categoria = {id};";

            // Base onde talvez o objeto da pessoa seja criada, caso algo retorne, começa como nula
            Categorias? categoria = null;

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                if (result.Read())
                {
                    // Pegar os dados lidos
                    string descricao = result.GetString(1);
                    int finalidade = result.GetInt32(2);

                    // Enviar um objeto com esses dados
                    categoria = new Categorias(id, descricao, finalidade);
                }
            });

            // Retornar o que foi achado na busca
            return categoria;
        }

        /// <summary>
        /// Volta toda a lista de categorias salvas no sistema
        /// </summary>
        /// <returns>Uma lista com todos os objetos que estiverem no sistema</returns>
        public List<Categorias> GetCategorias()
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"SELECT * FROM categorias;";

            // Lista que vai ter todas as entradas achadas 
            List<Categorias> categorias = [];

            // Mandar apagar as suas transações
            ConexaoBD.executarQuery(sqlQuery, (result) =>
            {
                // Tentar montar o objeto pessoa com esse resultado
                while (result.Read())
                {
                    // Pegar os dados lidos
                    int id = result.GetInt32(0);
                    string descricao = result.GetString(1);
                    int finalidade = result.GetInt32(2);

                    // Enviar um objeto com esses dados
                    categorias.Add(new Categorias(id, descricao, finalidade));
                }
            });

            // Simplesmente mostrar o conjunto todo
            return categorias;
        }

        /// <summary>
        /// Procura uma categoria do servidor com base no ID da tabela
        /// </summary>
        /// <param name="categoria">Novo objeto categoria que será salvo na lista</param>
        public void UpdateCategoria(Categorias categoria)
        {
            // Selecionar todas as entradas do conjunto
            string sqlQuery = $"UPDATE categorias SET Descricao='{categoria.Descricao}', Finalidade={categoria.Finalidade} WHERE Id_Categoria = {categoria.Id_Categoria};";

            // Mandar o pedido de atualização
            ConexaoBD.executarQuery(sqlQuery, null);
        }
    }
}
