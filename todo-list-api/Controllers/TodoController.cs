using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services.Todo;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
            // if (_todoService.TodoItems.Count() == 0)
            // {
            //     _todoService.TodoItems.Add(new TodoItem { Name = "Item1" });
            //     _todoService.SaveChanges();
            // }
        }

        [HttpGet]
        public ActionResult<List<TodoItemModel>> GetAllTodo()
        {
            return _todoService.GetAllTodo();
        }

    /*

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemModel>>> GetAllTodo()
    {
        return await _todoService.TodoItems.ToListAsync();
    }

    */

        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<TodoItemModel> GetTodo(string id)
        {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null) {
                return NotFound();
            }

            return todoExist;
        }

/*
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemModel>> GetTodoItem(long id)
    {
        var todoItem = await _todoService.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return todoItem;

    }
*/
        [HttpPost]
        public ActionResult<TodoItemModel> CreateTodo(TodoItemModel newTodo) {
            _todoService.CreateTodo(newTodo);

            return CreatedAtRoute("GetBook", new {id = newTodo.Id.ToString()}, newTodo);
        }



/*
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> PostTodoItem(TodoItemModel item)
        {
            _todoService.TodoItems.Add(item);
            await _todoService.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }
*/

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateTodo(string id, TodoItemModel updateTodo) {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null) {
                return NotFound();
            }

            _todoService.UpdateTodo(id, updateTodo);
            return NoContent();

        }

        

/*
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemModel>> PutTodoItem(long id, TodoItemModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _todoService.Entry(item).State = EntityState.Modified;
            await _todoService.SaveChangesAsync();
            return NoContent();
        }
*/

         [HttpDelete()]
        public IActionResult DeleteTodo(string id) {
            var todoExist = _todoService.GetTodo(id);
            if (todoExist == null) {
                return NotFound();
            }

            _todoService.RemoveTodo(id);
            return NoContent();

        }

/*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoService.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _todoService.TodoItems.Remove(todoItem);
            await _todoService.SaveChangesAsync();

            return NoContent();
        }
         */
    }
}