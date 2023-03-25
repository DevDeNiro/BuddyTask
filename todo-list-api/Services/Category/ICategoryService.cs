using System.Collections.Generic;
using TodoApi.Models;
using MongoDB.Driver;

namespace TodoApi.Service.ICategoryService
{
    public interface ICategoryService
    {
        List<CategoryItemModel> GetAllCategories();
        CategoryItemModel GetCategory(string id);
        CategoryItemModel CreateCategory(CategoryItemModel category);
        void UpdateCategory(string id, UpdateDefinition<CategoryItemModel> updateDefinition);
        void RemoveCategory(string id);
    }
}
