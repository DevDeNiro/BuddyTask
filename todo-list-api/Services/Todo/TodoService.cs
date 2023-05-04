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
        private readonly IMongoCollection<CategoryItemModel> _categories;


        public TodoService(MongoDbContext context)
        {
            _todos = context.Database.GetCollection<TodoItemModel>("TodoItems");
            _categories = context.Database.GetCollection<CategoryItemModel>("CategoryItems");
        }

        public List<TodoItemModel> GetAllTodo()
        {
            return _todos.Find(new BsonDocument()).ToList();
        }

        public TodoItemModel GetTodo(string id)
        {
            // Retrieve the corresponding todo
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            return _todos.Find(filter).FirstOrDefault();
        }

        public TodoItemModel CreateTodo(TodoItemModel todo)
        {
            _todos.InsertOne(todo);

            if (string.IsNullOrEmpty(todo.CategoryId))
            {
                return todo;
            }

            // Retrieve the corresponding category
            var categoryFilter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(todo.CategoryId));
            var category = _categories.Find(categoryFilter).FirstOrDefault();

            if (category != null)
            {
                category.TodoItems.Add(todo);

                // Update the category in the Category collection
                var updateDefinition = Builders<CategoryItemModel>.Update.Set(c => c.TodoItems, category.TodoItems);
                _categories.UpdateOne(categoryFilter, updateDefinition);
            }

            return todo;
        }

        public void UpdateTodo(string id, UpdateDefinition<TodoItemModel> updateDefinition)
        {
            // Retrieve the corresponding todo
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _todos.UpdateOne(filter, updateDefinition);
        }

        public void UpdateTodoFromCategory(string categoryId, string todoId, UpdateDefinition<TodoItemModel> updateDefinition)
        {
            var categoryFilter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(categoryId));
            var category = _categories.Find(categoryFilter).FirstOrDefault();

            if (category != null)
            {
                var todoItem = category.TodoItems.Find(t => t.Id == todoId);

                if (todoItem != null)
                {
                    // Update the TodoItem in the Todo collection
                    _todos.UpdateOne(Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(todoId)), updateDefinition);

                    int index = category.TodoItems.IndexOf(todoItem);
                    var updatedTodo = _todos.Find(Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(todoId))).FirstOrDefault();

                    category.TodoItems[index] = updatedTodo;
                    var categoryUpdateDefinition = Builders<CategoryItemModel>.Update
                                                   .Set(cat => cat.TodoItems, category.TodoItems);
                    _categories.UpdateOne(categoryFilter, categoryUpdateDefinition);
                }
            }
        }

        public void RemoveTodo(string id)
        {
            // Retrieve the corresponding todo
            var filter = Builders<TodoItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _todos.DeleteOne(filter);
        }

        public void RemoveTodoFromCategory(string categoryId, string todoId)
        {
            var categoryFilter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(categoryId));
            var category = _categories.Find(categoryFilter).FirstOrDefault();

            if (category != null)
            {
                var todoItem = category.TodoItems.Find(t => t.Id == todoId);

                if (todoItem != null)
                {
                    category.TodoItems.Remove(todoItem);
                    var categoryUpdateDefinition = Builders<CategoryItemModel>.Update
                                                   .Set(cat => cat.TodoItems, category.TodoItems);
                    _categories.UpdateOne(categoryFilter, categoryUpdateDefinition);
                }
            }
        }
    }
}
