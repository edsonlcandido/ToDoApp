using System.Data.SQLite;
using Dapper;

namespace ToDoApp.Server
{
    public class TarefaContext
    {
        private readonly string connectionString;

        public TarefaContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefas()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<Tarefa>("SELECT * FROM Tarefas");
            }
        }

        public async Task<Tarefa> ObterTarefa(string id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryFirstAsync<Tarefa>("SELECT * FROM Tarefas WHERE id = @Id", new { Id = id });
            }
        }

        public async Task AdicionarTarefa(Tarefa tarefa)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.ExecuteAsync("INSERT INTO Tarefas (Id, Descricao, DataCriacao, Concluido) VALUES (@Id, @Descricao, @DataCriacao, @Concluido)", tarefa);
            }
        }

        public async Task AtualizarTarefa(Tarefa tarefa)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.ExecuteAsync("UPDATE Tarefas SET Descricao = @Descricao, Concluido = @Concluido WHERE Id = @Id", tarefa);
            }
        }

        public async Task ExcluirTarefa(string id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                await connection.ExecuteAsync("DELETE FROM Tarefas WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
