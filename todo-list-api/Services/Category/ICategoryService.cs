using TodoApi.Models;
using MongoDB.Driver;

namespace TodoApi.Service.ICategoryService
{
    public interface ICategoryService
    {
        List<CategoryItemModel> GetAllCategories();
        CategoryItemModel GetCategory(string id);
        Task<CategoryItemModel?> GetCategoryAsync(string categoryId);
        CategoryItemModel CreateCategory(CategoryItemModel category);
        void UpdateCategory(string id, UpdateDefinition<CategoryItemModel> updateDefinition);
        Task<bool> RemoveCategory(string id);

    }
}
