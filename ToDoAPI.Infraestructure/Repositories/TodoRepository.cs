// Importamos EF Core para usar ToListAsync, FindAsync, etc.
using Microsoft.EntityFrameworkCore;
// Importamos la entidad Todo
using ToDoAPI.Domain.Entities;
// Importamos la interfaz que esta clase implementa
using ToDoAPI.Domain.Interfaces;
// Importamos el DbContext para acceder a la base de datos
using ToDoAPI.Infrastructure.Data;

namespace ToDoAPI.Infrastructure.Repositories;

// Esta clase IMPLEMENTA ITodoRepository — aquí sí sabemos cómo hablar con SQL Server
// Domain definió el contrato, nosotros lo cumplimos aquí
public class TodoRepository : ITodoRepository
{
    // El DbContext nos da acceso a las tablas de la base de datos
    private readonly AppDbContext _context;

    // Recibimos el DbContext por Inyección de Dependencias
    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    // SELECT * FROM Todos ORDER BY CreatedAt DESC
    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        return await _context.Todos
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    // SELECT * FROM Todos WHERE Id = @id
    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    // INSERT INTO Todos (Title, IsCompleted, CreatedAt) VALUES (...)
    public async Task<Todo> AddAsync(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
        return todo;
    }

    // UPDATE Todos SET Title = @title, IsCompleted = @isCompleted WHERE Id = @id
    // EF Core detecta los cambios automáticamente porque está "trackeando" el objeto
    public async Task<Todo> UpdateAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        return todo;
    }

    // DELETE FROM Todos WHERE Id = @id
    public async Task DeleteAsync(Todo todo)
    {
        _context.Todos.Remove(todo);
    }
}