using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Domain.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> AddAsync(Todo todo);
    Task<Todo?> UpdateAsync(Todo todo);
    Task DeleteAsync(Todo todo);
}