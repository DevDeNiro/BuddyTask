using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services.Todo
{
    public class TodoService
    {
        private readonly IMongoCollection<TodoItemModel> _todos;

        public TodoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("TodoDB"));
            var database = client.GetDatabase("TodoDB");
            _todos = database.GetCollection<TodoItemModel>("Todos");
        }

        public List<TodoItemModel> GetAllTodo()
        {
            return _todos.Find(todo => true).ToList();
        }

        public TodoItemModel GetTodo(string id)
        {
            return _todos.Find(todo => todo.Id == id).FirstOrDefault();
        }


        public TodoItemModel CreateTodo(TodoItemModel todo)
        {
            _todos.InsertOne(todo);
            return todo;
        }

        public void UpdateTodo(string id, TodoItemModel newTodo)
        {
            _todos.ReplaceOne(todo => todo.Id == id, newTodo);
        }

        public void RemoveTodo(string id)
        {
            _todos.DeleteOne(todo => todo.Id == id);
        }

    }
}