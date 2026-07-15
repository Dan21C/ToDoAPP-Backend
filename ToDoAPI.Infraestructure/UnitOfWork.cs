// Importamos la interfaz que esta clase implementa
using ToDoAPI.Domain.Interfaces;
// Importamos el DbContext para guardar los cambios en la base de datos
using ToDoAPI.Infrastructure.Data;
// Importamos el repositorio concreto de tareas
using ToDoAPI.Infrastructure.Repositories;

namespace ToDoAPI.Infrastructure;

// UnitOfWork implementa IUnitOfWork — es el "gerente" que definimos en Domain
// Agrupa todos los repositorios y controla cuándo se guardan los cambios en la DB
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    // Recibimos el DbContext por Inyección de Dependencias
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        // Creamos el repositorio pasándole el mismo DbContext
        // Así todos los repositorios comparten la misma conexión
        Todos = new TodoRepository(_context);
    }

    // Propiedad que expone el repositorio de tareas
    // El Service usa esto para acceder a las operaciones de la DB
    public ITodoRepository Todos { get; private set; }

    // Ejecuta todos los cambios pendientes en la base de datos en una sola transacción
    // Si algo falla, ningún cambio se guarda — o todo o nada
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}