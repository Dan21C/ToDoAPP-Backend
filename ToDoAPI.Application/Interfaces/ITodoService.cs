// Importamos los DTOs para poder usarlos como parámetros y retornos de los métodos
using ToDoAPI.Application.DTOs;

// Le decimos a .NET dónde vive esta interfaz
namespace ToDoAPI.Application.Interfaces;

// Contrato del servicio — define QUÉ operaciones existen, no CÓMO se hacen
// El Controller solo va a conocer esta interfaz, nunca la implementación concreta
public interface ITodoService
{
    // Trae todas las tareas — devuelve una lista de TodoResponseDto
    Task<IEnumerable<TodoResponseDto>> GetAllAsync();

    // Trae una tarea por su Id — el ? significa que puede devolver null si no existe
    Task<TodoResponseDto?> GetByIdAsync(int id);

    // Crea una tarea nueva — recibe solo el título (CreateTodoDto) y devuelve la tarea creada
    Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);

    // Actualiza una tarea — recibe el id a actualizar y los nuevos datos (UpdateTodoDto)
    // Devuelve null si el id no existe
    Task<TodoResponseDto?> UpdateAsync(int id, UpdateTodoDto dto);

    // Borra una tarea — devuelve true si la borró, false si el id no existía
    Task<bool> DeleteAsync(int id);
}