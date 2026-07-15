// Importamos MVC para usar ControllerBase y los atributos HTTP
using Microsoft.AspNetCore.Mvc;
// Importamos los DTOs para usarlos como parámetros y retornos
using ToDoAPI.Application.DTOs;
// Importamos la interfaz del servicio
using ToDoAPI.Application.Interfaces;

namespace ToDoAPI.API.Controllers;

// [Route] define la URL base: /api/todos
// [ApiController] activa validaciones automáticas del modelo
[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    // Usamos la interfaz, no la clase concreta
    private readonly ITodoService _service;

    // El servicio llega por Inyección de Dependencias
    public TodosController(ITodoService service)
    {
        _service = service;
    }

    // GET /api/todos — trae todas las tareas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos); // 200 + lista JSON
    }

    // GET /api/todos/5 — trae una tarea por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _service.GetByIdAsync(id);
        return todo is null ? NotFound() : Ok(todo); // 404 o 200
    }

    // POST /api/todos — crea una tarea nueva
    // Body: { "title": "Mi tarea" }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto dto)
    {
        var todo = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo); // 201
    }

    // PUT /api/todos/5 — actualiza una tarea
    // Body: { "title": "Mi tarea", "isCompleted": true }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoDto dto)
    {
        var todo = await _service.UpdateAsync(id, dto);
        return todo is null ? NotFound() : Ok(todo); // 404 o 200
    }

    // DELETE /api/todos/5 — borra una tarea
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(); // 204 o 404
    }
}