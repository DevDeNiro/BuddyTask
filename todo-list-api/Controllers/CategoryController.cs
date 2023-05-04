using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service.ICategoryService;
using MongoDB.Driver;
using TodoApi.Service.ITodoService;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ITodoService _todoService;

        public CategoryController(ICategoryService categoryService, ITodoService todoService)
        {
            _categoryService = categoryService;
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<CategoryItemModel>> Get()
        {
            return _categoryService.GetAllCategories();
        }


        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryItemModel>> GetCategory(string id)
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        public ActionResult<CategoryItemModel> Create(CategoryItemModel category)
        {
            if (category == null)
            {
                return BadRequest("Category item is null.");
            }

            _categoryService.CreateCategory(category);

            if (category.Id != null)
            {
                return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
            }
            else
            {
                return StatusCode(500, "Failed to create the Category Item");
            }
        }

        [HttpPut("{id}", Name = "PutCategory")]
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

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            // Delete the category and associated Todo items
            bool success = await _categoryService.RemoveCategory(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
