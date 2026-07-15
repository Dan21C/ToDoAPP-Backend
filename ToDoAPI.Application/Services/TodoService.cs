// Importamos los DTOs que usamos como parámetros y retornos
using ToDoAPI.Application.DTOs;
// Importamos la interfaz que esta clase implementa
using ToDoAPI.Application.Interfaces;
// Importamos la entidad Todo para poder crear nuevos objetos
using ToDoAPI.Domain.Entities;
// Importamos IUnitOfWork para acceder al repositorio y guardar cambios
using ToDoAPI.Domain.Interfaces;

namespace ToDoAPI.Application.Services;

// Esta clase IMPLEMENTA ITodoService — cumple el contrato definido en la interfaz
// Aquí vive la lógica de negocio, no en el Controller ni en el Repository
public class TodoService : ITodoService
{
    // IUnitOfWork nos da acceso al repositorio y al SaveChanges
    // readonly = solo se puede asignar en el constructor, nunca después
    private readonly IUnitOfWork _unitOfWork;

    // El constructor recibe IUnitOfWork por Inyección de Dependencias
    // .NET lo crea y nos lo da automáticamente, nosotros no hacemos new IUnitOfWork()
    public TodoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Trae todas las tareas y las convierte a DTOs antes de devolverlas
    public async Task<IEnumerable<TodoResponseDto>> GetAllAsync()
    {
        var todos = await _unitOfWork.Todos.GetAllAsync();
        // Select es como un "foreach" que transforma cada Todo en un TodoResponseDto
        return todos.Select(MapToDto);
    }

    // Busca una tarea por Id — si no existe devuelve null
    public async Task<TodoResponseDto?> GetByIdAsync(int id)
    {
        var todo = await _unitOfWork.Todos.GetByIdAsync(id);
        // Si todo es null devuelve null, si existe lo convierte a DTO
        return todo is null ? null : MapToDto(todo);
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        // Creamos la entidad Todo a partir del DTO — solo necesitamos el Title
        // IsCompleted y CreatedAt tienen valores por defecto en la entidad
        var todo = new Todo { Title = dto.Title };
        var created = await _unitOfWork.Todos.AddAsync(todo);
        // SaveChangesAsync ejecuta el INSERT en la base de datos
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(created);
    }

    public async Task<TodoResponseDto?> UpdateAsync(int id, UpdateTodoDto dto)
    {
        var todo = await _unitOfWork.Todos.GetByIdAsync(id);
        // Si no existe la tarea, devolvemos null — el Controller devolverá 404
        if (todo is null) return null;

        // Actualizamos los campos de la entidad con los datos del DTO
        todo.Title = dto.Title;
        todo.IsCompleted = dto.IsCompleted;

        await _unitOfWork.Todos.UpdateAsync(todo);
        // SaveChangesAsync ejecuta el UPDATE en la base de datos
        await _unitOfWork.SaveChangesAsync();
        return MapToDto(todo);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _unitOfWork.Todos.GetByIdAsync(id);
        // Si no existe devolvemos false — el Controller devolverá 404
        if (todo is null) return false;

        await _unitOfWork.Todos.DeleteAsync(todo);
        // SaveChangesAsync ejecuta el DELETE en la base de datos
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    // Método privado que convierte una entidad Todo en un TodoResponseDto
    // Lo usamos en todos los métodos para no repetir el mismo código (principio DRY)
    private static TodoResponseDto MapToDto(Todo todo) =>
        new(todo.Id, todo.Title, todo.IsCompleted, todo.CreatedAt);
}