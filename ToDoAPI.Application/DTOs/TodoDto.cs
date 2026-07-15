namespace ToDoAPI.Application.DTOs;

public record CreateTodoDto(string Title);//lo que el cliente manda en el POST. Solo el título.
public record UpdateTodoDto(string Title, bool IsCompleted);// lo que el cliente manda en el PUT. Título y si está completada.
public record TodoResponseDto(int Id, string Title, bool IsCompleted, DateTime CreatedAt);//lo que el API devuelve al cliente. Todo: Id, título, estado y fecha.