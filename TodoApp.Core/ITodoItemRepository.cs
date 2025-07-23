using TodoApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task AddAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(int id);
    }
}