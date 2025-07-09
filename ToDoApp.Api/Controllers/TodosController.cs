using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Business.IServices;
using ToDoApp.Api.TodosDtos;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {

        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var todo = await _todoService.CreateTodo(
                    createTodoDto.Title,
                    createTodoDto.Description,
                    createTodoDto.Priority,
                    createTodoDto.Category,
                    createTodoDto.DueDate,
                    createTodoDto.UserId
                );
                return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoById(id);
                if (todo == null)
                    return NotFound($"Todo with ID {id} not found.");

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            try
            {
                var todos = await _todoService.GetAllTodos();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTodosByUser(int userId)
        {
            try
            {
                var todos = await _todoService.GetTodosByUserId(userId);
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetTodosByCategory(string category)
        {
            try
            {
                var todos = await _todoService.GetTodosByCategory(category);
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("priority/{priority}")]
        public async Task<IActionResult> GetTodosByPriority(int priority)
        {
            try
            {
                var todos = await _todoService.GetTodosByPriority(priority);
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var todo = await _todoService.UpdateTodo(
                    id,
                    updateTodoDto.Title,
                    updateTodoDto.Description,
                    updateTodoDto.Priority,
                    updateTodoDto.Category,
                    updateTodoDto.DueDate,
                    updateTodoDto.IsCompleted
                );

                if (todo == null)
                    return NotFound($"Todo with ID {id} not found.");

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkTodoAsCompleted(int id)
        {
            try
            {
                var success = await _todoService.MarkTodoAsCompleted(id);
                if (!success)
                    return NotFound($"Todo with ID {id} not found.");

                return Ok(new { message = "Todo marked as completed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                var success = await _todoService.DeleteTodo(id);
                if (!success)
                    return NotFound($"Todo with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTodos([FromQuery] string query)
        {
            if (query == null || query == "")
            {
                return BadRequest("Query parameter cannot be null.");
            }

            try
            {
                var results = await _todoService.SearchTodos(query);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("overdue")]

        public async Task<IActionResult> GetOverdueTodos()
        {
            var results = await _todoService.GetOverdueTodos();
            return Ok(results);
        }

    }


}
