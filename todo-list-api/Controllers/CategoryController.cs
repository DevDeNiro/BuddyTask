using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service.ICategoryService;
using MongoDB.Driver;
using TodoApi.Data;
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

        // [HttpGet("{id}", Name = "GetCategory")]
        // public ActionResult<CategoryItemModel> Get(string id)
        // {
        //     var category = _categoryService.GetCategory(id);

        //     if (category == null)
        //     {
        //         return NotFound();
        //     }

        //     return category;
        // }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryItemModel>> GetCategory(string id)
        {
            var category = await _todoService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [HttpPost]
        public ActionResult<CategoryItemModel> Create(CategoryItemModel category)
        {
            _categoryService.CreateCategory(category);
            return CreatedAtRoute("GetCategory", new { id = category.Id.ToString() }, category);
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

        // [HttpDelete("{id}", Name = "DeleteCategory")]
        // public IActionResult Delete(string id)
        // {
        //     var category = _categoryService.GetCategory(id);

        //     if (category == null)
        //     {
        //         return NotFound();
        //     }

        //     _categoryService.RemoveCategory(id);

        //     return NoContent();
        // }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            // Supprime la catégorie et les éléments Todo associés
            bool success = await _todoService.RemoveCategoryAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
