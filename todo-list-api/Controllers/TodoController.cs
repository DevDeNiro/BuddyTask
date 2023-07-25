using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using MongoDB.Driver;
using TodoApi.Service.ICategoryService;
using TodoApi.Service.ITodoService;

namespace TodoApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ICategoryService _categoryService;

        public TodoController(ITodoService todoService, ICategoryService categoryService)
        {
            _todoService = todoService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<TodoItemModel>> Get()
        {
            return _todoService.GetAllTodo();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItemModel> Get(string id)
        {
            var todo = _todoService.GetTodo(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }


        [HttpPost]
        public ActionResult<TodoItemModel> CreateTodo(TodoItemModel todo)
        {
            // Check if todo is null
            if (todo == null)
            {
                return BadRequest("Todo item is null.");
            }

            // Check if CategoryId is not null or empty
            if (string.IsNullOrEmpty(todo.CategoryId))
            {
                return BadRequest("Category ID is missing.");
            }

            // Check if the category exists
            var category = _categoryService.GetCategory(todo.CategoryId);
            if (category == null)
            {
                return BadRequest("Category does not exist.");
            }

            _todoService.CreateTodo(todo);
            // _categoryService.AddTodoItemToCategory(todo.CategoryId, todo);

            // Check if todo.Id is not null
            if (todo.Id != null)
            {
                return CreatedAtRoute("GetTodo", new { id = todo.Id.ToString() }, todo);
            }
            else
            {
                return StatusCode(500, "Failed to create the Todo item.");
            }
        }

        [HttpPut("{id}", Name = "UpdateTodo")]
        public async Task<IActionResult> UpdateTodo(string id, [FromBody] TodoItemModel updateTodo)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            var updateDefinition = Builders<TodoItemModel>.Update
                .Set(todo => todo.Name, updateTodo.Name)
                .Set(todo => todo.Description, updateTodo.Description)
                .Set(todo => todo.StartDate, updateTodo.StartDate)
                .Set(todo => todo.EndDate, updateTodo.EndDate)
                .Set(todo => todo.Completed, updateTodo.Completed)
                .Set(todo => todo.CategoryId, updateTodo.CategoryId);

            _todoService.UpdateTodo(id, updateDefinition);

            // Check if CategoryId has changed
            if (todoExist.CategoryId != updateTodo.CategoryId)
            {
                // Remove todo from old category
                if (!string.IsNullOrEmpty(todoExist.CategoryId))
                {
                    await _categoryService.RemoveTodoItemFromCategory(todoExist.CategoryId, id);
                }

                // Add todo to new category
                if (!string.IsNullOrEmpty(updateTodo.CategoryId))
                {

                    await _categoryService.AddTodoItemToCategory(updateTodo.CategoryId, id);
                    await _categoryService.UpdateTodoItemInCategory(updateTodo.CategoryId, id, updateDefinition);

                }
            }
            else
            {
                // If CategoryId did not change, update TodoItem in the existing Category
                if (!string.IsNullOrEmpty(todoExist.CategoryId))
                {
                    await _categoryService.UpdateTodoItemInCategory(todoExist.CategoryId, id, updateDefinition);
                }
            }

            _todoService.UpdateTodo(id, updateDefinition);
            // If the todo is associated with a category, also update the todo in the category
            string categoryId = todoExist.CategoryId ?? string.Empty;
            if (!string.IsNullOrEmpty(categoryId))
            {
                await _categoryService.UpdateTodoItemInCategory(categoryId, id, updateDefinition);
            }

            var updatedTodo = _todoService.GetTodo(id);

            return Ok(updatedTodo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(string id)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            _todoService.RemoveTodo(id);

            // Delete the corresponding TodoItem in the Category
            string categoryId = todoExist.CategoryId ?? string.Empty;
            if (!string.IsNullOrEmpty(categoryId))
            {
                _categoryService.RemoveTodoItemFromCategory(categoryId, id);
            }

            return NoContent();
        }

    }
}