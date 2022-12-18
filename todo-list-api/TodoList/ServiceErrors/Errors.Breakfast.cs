using ErrorOr;

namespace TodoList.ServiceErrors;

public static class Errors
{
    public static class Todo
    {
        public static Error InvalidName => Error.Validation(
            code: "Todo.InvalidName",
            description: $"Todo name must be at least {Models.Todo.MinNameLength}" +
                $" characters long and at most {Models.Todo.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Todo.InvalidDescription",
            description: $"Todo description must be at least {Models.Todo.MinDescriptionLength}" +
                $" characters long and at most {Models.Todo.MaxDescriptionLength} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Todo.NotFound",
            description: "Todo not found");
    }
}