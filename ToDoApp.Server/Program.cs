using ToDoApp.Server;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<TarefaContext>(provider =>
    new TarefaContext("DataSource=Todo.db;"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo api", Version = "v1" });

});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo api V1");
    c.RoutePrefix = "swagger";
});

app.MapGet("/api/v1/tarefas", async (IServiceProvider services) =>
{
    var db = services.GetRequiredService<TarefaContext>();
    var tarefas = await db.ObterTarefas();
    return Results.Ok(tarefas);
});

app.MapPost("/api/v1/tarefas", async (IServiceProvider services,TarefaCreateModel model) =>
{
    var tarefa = new Tarefa { 
        Id = Guid.NewGuid().ToString("D"),
        Descricao = model.Descricao,
        DataCriacao = DateTime.Now,
        Concluido = false
    };

    var db = services.GetRequiredService<TarefaContext>();
    await db.AdicionarTarefa(tarefa);
    return Results.Created($"/api/v1/tarefas/{tarefa.Id}", tarefa);
});

app.MapDelete("/api/v1/tarefas/{id}", async (IServiceProvider services, string id) =>
{
    var db = services.GetRequiredService<TarefaContext>();
    await db.ExcluirTarefa(id);
    return Results.NoContent();
});

app.MapPut("/api/v1/tarefas/{id}", async (IServiceProvider services, string id, TarefaUpdateModel model) =>
{
    var db = services.GetRequiredService<TarefaContext>();
    Tarefa tarefa = new Tarefa { 
        Id = id,
        Descricao = model.Descricao,
        Concluido = model.Concluido
    };

    await db.AtualizarTarefa(tarefa);
    return Results.NoContent();
});

app.MapGet("/api/v1/tarefas/{id}", async (IServiceProvider services, string id) =>
{
    var db = services.GetRequiredService<TarefaContext>();
    var tarefas = await db.ObterTarefa(id);
    return Results.Ok(tarefas);
});

app.Run();

