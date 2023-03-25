using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Service.ITodoService;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using TodoApi.Data;


namespace TodoApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
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
        public ActionResult<TodoItemModel> Create([NotNull] TodoItemModel todo)
        {
            _todoService.CreateTodo(todo);

            return CreatedAtRoute("GetTodo", new { id = todo.Id.ToString() }, todo);
        }

        [HttpPut("{id}", Name = "UpdateTodo")]
        public IActionResult UpdateTodo(string id, TodoItemModel updateTodo)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null)
            {
                return NotFound();
            }

            // Créez un objet de mise à jour en ne modifiant que les champs "Name" et "DueDate"
            var updateDefinition = Builders<TodoItemModel>.Update
                .Set(todo => todo.Name, updateTodo.Name)
                .Set(todo => todo.Completed, updateTodo.Completed)
                .Set(todo => todo.DueDate, updateTodo.DueDate);

            // Modifiez la méthode "UpdateTodo" de votre service pour accepter un "UpdateDefinition"
            _todoService.UpdateTodo(id, updateDefinition);

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