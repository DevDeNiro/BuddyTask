using TodoList.ServiceErrors;
using TodoList.Models;
using ErrorOr;
using TodoList.Services.Todo;

namespace TodoList.Services.Todos;

public class Todo : ITodoService
{
    private static readonly Dictionary<Guid, Todo> _todo = new();


    public ErrorOr<Created> CreateTodo(Todo todo)
    {
        _todo.Add(todo.Id, todo);

        return Result.Created;
    }

    public ErrorOr<Created> CreateTodos(Todo todo)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<Deleted> DeleteTodo(Guid id)
    {
        _todo.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Todo> GetTodo(Guid id)
    {
        if (_todo.TryGetValue(id, out var Todo))
        {
            return Todo;
        }

        return Errors.Todo.NotFound;
    }

    public ErrorOr<UpsertedTodo> UpsertTodo(Todo todo)
    {
        var isNewlyCreated = !_todo.ContainsKey(todo.Id);
        _todo[todo.Id] = todo;

        return new UpsertedTodo(isNewlyCreated);
    }

}
