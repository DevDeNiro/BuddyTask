namespace TodoList.Contracts.Todo;

public record CreateTodoRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime);