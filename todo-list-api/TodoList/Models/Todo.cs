using TodoList.Contracts.Todo;
using TodoList.ServiceErrors;
using ErrorOr;

namespace TodoList.Models;

public class Todo
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime LastModifiedDateTime { get; }

    private Todo(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime? endDateTime,
        DateTime lastModifiedDateTime
        )
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
    }

    public static ErrorOr<Todo> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        Guid? id = null)
    {
        List<Error> errors = new();

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Todo.InvalidName);
        }

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Todo.InvalidDescription);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Todo(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow);
    }

    public static ErrorOr<Todo> From(CreateTodoRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime);
    }

    public static ErrorOr<Todo> From(Guid id, UpsertTodoRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            id);
    }
}