namespace ToDoApp.Server
{
    public class Tarefa
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Concluido { get; set; }
    }

    public class TarefaCreateModel
    {
        public string Descricao { get; set; }
    }

    public class TarefaUpdateModel
    {
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
    }
}
