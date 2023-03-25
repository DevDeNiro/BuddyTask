using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service.ICategoryService;
using MongoDB.Driver;
using TodoApi.Data;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<CategoryItemModel>> Get()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<CategoryItemModel> Get(string id)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/Category
        [HttpPost]
        public ActionResult<CategoryItemModel> Create(CategoryItemModel category)
        {
            _categoryService.CreateCategory(category);
            return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, CategoryItemModel categoryIn)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var updateDefinition = Builders<CategoryItemModel>.Update
                .Set(c => c.Name, categoryIn.Name);

            _categoryService.UpdateCategory(id, updateDefinition);

            return NoContent();
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryService.RemoveCategory(id);

            return NoContent();
        }
    }
}
