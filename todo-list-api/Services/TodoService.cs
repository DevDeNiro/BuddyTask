using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services.Todo {
  public class TodoService {
    private readonly IMongoCollection<TodoItemModel> _todos;

    public TodoService (IConfiguration config) {
      var client = new MongoClient (config.GetConnectionString ("TodoDB"));
      var database = client.GetDatabase ("TodoDB");
      _todos = database.GetCollection<TodoItemModel> ("Todos");
    }

    // Read all todo
    public List<TodoItemModel> GetAllTodo () {
      return _todos.Find (book => true).ToList ();
    }

    public TodoItemModel CreateTodo (TodoItemModel todo) {
      _todos.InsertOne (todo);
      return todo;
    }

    public void UpdateTodo (string id, TodoItemModel newTodo) {
      _todos.ReplaceOne (book => book.Id == id, newTodo);
    }

    public void RemoveTodo (string id) {
      _todos.DeleteOne (book => book.Id == id);
    }

    // Est ce vraiment utile ???
    public TodoItemModel GetTodo (string id) {
      return _todos.Find (book => book.Id == id).FirstOrDefault ();
    }

    public void RemoveOldTodo (TodoItemModel oldTodo) {
      _todos.DeleteOne (book => book.Id == oldTodo.Id);
    }
  }
}