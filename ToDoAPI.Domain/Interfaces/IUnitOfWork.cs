
namespace ToDoAPI.Domain.Interfaces;
public interface IUnitOfWork
{
    ITodoRepository Todos { get; }
    Task<int> SaveChangesAsync();
}
