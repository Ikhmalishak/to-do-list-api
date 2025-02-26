using Microsoft.AspNetCore.Mvc;
using TodoApi.Application;
using TodoApi.Core;

namespace TodoApi.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _todoService.GetTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] TodoItem todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Title)) return BadRequest("Title is required.");
            var createdTodo = await _todoService.AddTodoAsync(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoItem todo)
        {
            var updatedTodo = await _todoService.UpdateTodoAsync(id, todo);
            if (updatedTodo == null) return NotFound();
            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var success = await _todoService.DeleteTodoAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}