using TodoList.Contracts.Todo;
using TodoList.Models;
using TodoList.ServiceErrors;
using TodoList.Services.Todos;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

public class TodoController : ApiController
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public IActionResult CreateTodo(CreateTodoRequest request)
    {
        ErrorOr<Models.Todo> requestToTodoResult = Models.Todo.From(request);

        if (requestToTodoResult.IsError)
        {
            return Problem(requestToTodoResult.Errors);
        }

        var todo = requestToTodoResult.Value;
        ErrorOr<Created> createTodoResult = _todoService.CreateTodoRequest(todo);

        return createTodoResult.Match(
            created => CreatedAtGetTodo(todo),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTodo(Guid id)
    {
        ErrorOr<Models.Todo> getTodoResult = _todoService.GetTodo(id);

        return getTodoResult.Match(
            todo => Ok(MapTodoResponse(todo)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertTodo(Guid id, UpsertTodoRequest request)
    {
        ErrorOr<Models.Todo> requestToTodoResult = Todo.From(id, request);

        if (requestToTodoResult.IsError)
        {
            return Problem(requestToTodoResult.Errors);
        }

        var todo = requestToTodoResult.Value;
        ErrorOr<UpsertedTodo> upsertTodoResult = _todoService.UpsertTodo(todo);

        return upsertTodoResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetTodo(todo) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTodo(Guid id)
    {
        ErrorOr<Deleted> deleteTodoResult = _todoService.DeleteTodo(id);

        return deleteTodoResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    private static TodoResponse MapTodoResponse(Todo todo)
    {
        return new TodoResponse(
            todo.Id,
            todo.Name,
            todo.Description,
            todo.StartDateTime,
            todo.EndDateTime,
            todo.LastModifiedDateTime);
    }

    private CreatedAtActionResult CreatedAtGetTodo(Todo todo)
    {
        return CreatedAtAction(
            actionName: nameof(GetTodo),
            routeValues: new { id = todo.Id },
            value: MapTodoResponse(todo));
    }
}