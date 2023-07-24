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

        public void UpdateCategory(string id, UpdateDefinition<CategoryItemModel> updateDefinition)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _categories.UpdateOne(filter, updateDefinition);
        }

        public async Task RemoveTodoItemFromCategory(string categoryId, string todoItemId)
        {
            var category = await _categories.Find(c => c.Id == categoryId).FirstOrDefaultAsync();
            if (category != null)
            {
                var todoItem = category.TodoItems.FirstOrDefault(t => t.Id == todoItemId);
                if (todoItem != null)
                {
                    category.TodoItems.Remove(todoItem);
                    var updateDefinition = Builders<CategoryItemModel>.Update.Set(c => c.TodoItems, category.TodoItems);
                    await _categories.UpdateOneAsync(c => c.Id == categoryId, updateDefinition);
                }
            }
        }
        public async Task AddTodoItemToCategory(string categoryId, string todoItemId)
        {
            var category = await _categories.Find(c => c.Id == categoryId).FirstOrDefaultAsync();
            var todoItem = await _todos.Find(t => t.Id == todoItemId).FirstOrDefaultAsync();
            if (category != null && todoItem != null)
            {
                category.TodoItems.Add(todoItem);
                var updateDefinition = Builders<CategoryItemModel>.Update.Set(c => c.TodoItems, category.TodoItems);
                await _categories.UpdateOneAsync(c => c.Id == categoryId, updateDefinition);
            }
        }

        public async Task UpdateTodoItemInCategory(string categoryId, string todoItemId, UpdateDefinition<TodoItemModel> updateDefinition)
        {
            var category = await _categories.Find(c => c.Id == categoryId).FirstOrDefaultAsync();
            if (category != null)
            {
                var todoItemIndex = category.TodoItems.FindIndex(t => t.Id == todoItemId);
                if (todoItemIndex != -1)
                {
                    // Assuming that the updated TodoItemModel has already been saved in the _todos collection
                    var updatedTodo = await _todos.Find(t => t.Id == todoItemId).FirstOrDefaultAsync();

                    // Replace the old item with the updated one
                    category.TodoItems[todoItemIndex] = updatedTodo;

                    var categoryUpdateDefinition = Builders<CategoryItemModel>.Update.Set(c => c.TodoItems, category.TodoItems);
                    await _categories.UpdateOneAsync(c => c.Id == categoryId, categoryUpdateDefinition);
                }
            }
        }


    }
}
