namespace TodoList.Contracts.Todo;

public record UpsertTodoRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime);