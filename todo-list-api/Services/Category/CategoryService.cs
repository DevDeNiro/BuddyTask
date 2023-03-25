using System.Collections.Generic;
using TodoApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using TodoApi.Data;
using TodoApi.Service.ITodoService;

namespace TodoApi.Service.ICategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<CategoryItemModel> _categories;

        public CategoryService(MongoDbContext context)
        {
            _categories = context.Database.GetCollection<CategoryItemModel>("CategoryItems");
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

        public void UpdateCategory(string id, UpdateDefinition<CategoryItemModel> updateDefinition)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _categories.UpdateOne(filter, updateDefinition);
        }

        public void RemoveCategory(string id)
        {
            var filter = Builders<CategoryItemModel>.Filter.Eq("_id", ObjectId.Parse(id));
            _categories.DeleteOne(filter);
        }
    }
}
