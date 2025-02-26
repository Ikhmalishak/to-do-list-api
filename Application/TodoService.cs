using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Core;
using TodoApi.Infrastructure;

namespace TodoApi.Application
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            return _context.Todos.ToList();
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return _context.Todos.FirstOrDefault(t => t.Id == id);
        }

        public async Task<TodoItem> AddTodoAsync(TodoItem todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<TodoItem?> UpdateTodoAsync(int id, TodoItem updatedTodo)
        {
            var existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null) return null;

            existingTodo.Title = updatedTodo.Title;
            existingTodo.IsCompleted = updatedTodo.IsCompleted;
            await _context.SaveChangesAsync();

            return existingTodo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return false;

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
