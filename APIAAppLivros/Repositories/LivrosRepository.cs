using APIAAppLivros.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace LivrosAPI.Repositories
{
    public class LivrosRepository
    {
        private readonly string _connectionString;

        public LivrosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Livros>> ListarTodosLivros(bool? ativo = null)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM tb_livros";

                if (ativo.HasValue)
                {
                    sql += " WHERE Ativo = @Ativo";
                    return await conn.QueryAsync<Livros>(sql, new { Ativo = ativo });
                }
                else
                {
                    return await conn.QueryAsync<Livros>(sql);
                }
            }
        }
        public async Task<Livros> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM tb_livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livros>(sql, new { Id = id });
            }
        }

        public async Task<int> DeletarPorId(int id)
        {
            var sql = "DELETE FROM tb_livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<int> Salvar(Livros dados)
        {
            var sql = "INSERT INTO tb_livros(Titulo ,Autor ,AnoPublicacao ,Genero ,NumeroPaginas ) " +
                "values (@Titulo ,@Autor ,@AnoPublicacao ,@Genero ,@NumeroPaginas )";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql,
                    new
                    {
                        Titulo = dados.Titulo,
                        Autor = dados.Autor,
                        AnoPublicacao = dados.AnoPublicacao,
                        Genero = dados.Genero,
                        NumeroPaginas = dados.NumeroPaginas,
                      
                    });
            }
        }

        public async Task<int> Atualizar(Livros dados)
        {
            var sql = "UPDATE tb_livros set Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao, Genero = @Genero,NumeroPaginas = @NumeroPaginas WHERE Id = @id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }

    }
}
