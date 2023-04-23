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
        Task<bool> RemoveCategory(string id);
        Task<CategoryItemModel?> GetCategoryAsync(string categoryId);
        void UpdateTodoItemInCategory(string categoryId, string todoId, TodoItemModel todoIn);
        void DeleteTodoItemInCategory(string categoryId, string todoId);
        void AddTodoItemToCategory(string categoryId, TodoItemModel todo);

    }
}
