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

    }
}
