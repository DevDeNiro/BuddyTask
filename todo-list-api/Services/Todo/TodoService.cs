using TodoApi.Models;
using TodoApi.Service.ITodoService;
using MongoDB.Driver;
using MongoDB.Bson;
using TodoApi.Data;

namespace TodoApi.Service.ITodoService
{
    public class TodoService : ITodoService
    {
        private readonly IMongoCollection<TodoItemModel> _todos;

        public TodoService(MongoDbContext context)
        {
            _todos = context.Database.GetCollection<TodoItemModel>("TodoItems");
        }

        public List<TodoItemModel> GetAllTodo()
        {
            return _todos.Find(new BsonDocument()).ToList();
        }

        public TodoItemModel GetTodo(string id)
        {
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            return _todos.Find(filter).FirstOrDefault();
        }

        public TodoItemModel CreateTodo(TodoItemModel todo)
        {
            _todos.InsertOne(todo);
            return todo;
        }

        public void UpdateTodo(string id, UpdateDefinition<TodoItemModel> updateDefinition)
        {
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _todos.UpdateOne(filter, updateDefinition);
        }

        public void RemoveTodo(string id)
        {
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _todos.DeleteOne(filter);
        }
    }
}
