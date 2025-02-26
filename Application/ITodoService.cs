using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetTodosAsync();
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<TodoItem> AddTodoAsync(TodoItem todo);
        Task<TodoItem?> UpdateTodoAsync(int id, TodoItem todo);
        Task<bool> DeleteTodoAsync(int id);
    }
}
