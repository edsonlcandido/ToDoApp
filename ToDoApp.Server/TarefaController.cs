using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Server
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;

        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Tarefa>> ObterTarefas()
        {
            return await _context.ObterTarefas();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa([FromBody] Tarefa tarefa)
        {
            await _context.AdicionarTarefa(tarefa);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(string id, [FromBody] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            await _context.AtualizarTarefa(tarefa);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTarefa(string id)
        {
            await _context.ExcluirTarefa(id);

            return Ok();
        }
    }
}
