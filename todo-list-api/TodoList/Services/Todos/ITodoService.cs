using TodoList.Models;
using ErrorOr;
using TodoList.Services.Todo;

namespace TodoList.Services.Todos;

public interface ITodoService
{
    ErrorOr<Created> CreateTodos(Todo todo);
    ErrorOr<Todo> GetTodo(Guid id);
    ErrorOr<UpsertedTodo> UpsertTodo(Todo todo);
    ErrorOr<Deleted> DeleteTodo(Guid id);
    ErrorOr<Created> CreateTodoRequest(Models.Todo todo);
}