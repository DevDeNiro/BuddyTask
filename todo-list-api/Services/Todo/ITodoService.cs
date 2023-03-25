using TodoApi.Models;
using MongoDB.Driver;

namespace TodoApi.Service.ITodoService
{
    public interface ITodoService
    {
        List<TodoItemModel> GetAllTodo();
        TodoItemModel GetTodo(string id);
        TodoItemModel CreateTodo(TodoItemModel todo);
        void UpdateTodo(string id, UpdateDefinition<TodoItemModel> updateDefinition);
        void RemoveTodo(string id);
    }
}
