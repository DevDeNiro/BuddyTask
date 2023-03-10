using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services.Todo;

namespace TodoApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<TodoItemModel>> GetAllTodo()
        {
            return _todoService.GetAllTodo();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItemModel> GetTodo(string id)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            return todoExist;
        }

        [HttpPost]
        public ActionResult<TodoItemModel> CreateTodo(TodoItemModel newTodo)
        {
            _todoService.CreateTodo(newTodo);

            return CreatedAtRoute("GetTodo", new { id = newTodo.Id.ToString() }, newTodo);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateTodo(string id, TodoItemModel updateTodo)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            _todoService.UpdateTodo(id, updateTodo);
            return NoContent();

        }

        [HttpDelete()]
        public IActionResult DeleteTodo(string id)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            _todoService.RemoveTodo(id);
            return NoContent();

        }

    }
}