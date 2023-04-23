using TodoApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using TodoApi.Data;

namespace TodoApi.Service.ICategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<CategoryItemModel> _categories;
        private readonly IMongoCollection<TodoItemModel> _todos;

        public CategoryService(MongoDbContext context)
        {
            _categories = context.Database.GetCollection<CategoryItemModel>("CategoryItems");
            _todos = context.Database.GetCollection<TodoItemModel>("TodoItems");

        }

        public List<CategoryItemModel> GetAllCategories()
        {
            return _categories.Find(new BsonDocument()).ToList();
        }

        public CategoryItemModel GetCategory(string id)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            return _categories.Find(filter).FirstOrDefault();
        }

        public CategoryItemModel CreateCategory(CategoryItemModel category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public async Task<bool> RemoveCategory(string categoryId)
        {
            // Delete category
            var categoryDeleteResult = await _categories.DeleteOneAsync(x => x.Id == categoryId);

            if (categoryDeleteResult.DeletedCount == 0)
            {
                return false;
            }

            // Deletes all items associated with the category
            await _todos.DeleteManyAsync(x => x.CategoryId == categoryId);

            return true;
        }

        public async Task<CategoryItemModel?> GetCategoryAsync(string categoryId)
        {
            var category = await _categories.Find(x => x.Id == categoryId).FirstOrDefaultAsync();

            if (category != null)
            {
                // Load Todo items associated with this category
                category.TodoItems = await _todos.Find(x => x.CategoryId == categoryId).ToListAsync();
            }

            return category;
        }

        public void AddTodoItemToCategory(string categoryId, TodoItemModel todo)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(categoryId));
            var update = Builders<CategoryItemModel>.Update.Push("TodoItems", todo);
            _categories.UpdateOne(filter, update);
        }

        public void UpdateCategory(string id, UpdateDefinition<CategoryItemModel> updateDefinition)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _categories.UpdateOne(filter, updateDefinition);
        }

        public void UpdateTodoItemInCategory(string categoryId, string todoId, TodoItemModel todoIn)
        {
            var category = GetCategory(categoryId);

            if (category != null)
            {
                var todoItem = category.TodoItems.Find(t => t.Id == todoId);

                if (todoItem != null)
                {
                    int index = category.TodoItems.IndexOf(todoItem);
                    category.TodoItems[index] = todoIn;
                    var updateDefinition = Builders<CategoryItemModel>.Update
                                           .Set(cat => cat.TodoItems, category.TodoItems);
                    UpdateCategory(categoryId, updateDefinition);
                }
            }
        }

        public void DeleteTodoItemInCategory(string categoryId, string todoId)
        {
            var category = GetCategory(categoryId);

            if (category != null)
            {
                var todoItem = category.TodoItems.Find(t => t.Id == todoId);

                if (todoItem != null)
                {
                    category.TodoItems.Remove(todoItem);
                    var updateDefinition = Builders<CategoryItemModel>.Update
                                           .Set(cat => cat.TodoItems, category.TodoItems);
                    UpdateCategory(categoryId, updateDefinition);
                }
            }
        }
    }
}
