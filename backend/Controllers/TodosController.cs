using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Interfaces;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    /// <summary>
    /// Controller for managing Todo items
    /// Provides RESTful API endpoints for CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodosController> _logger;
        
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }
        
        /// <summary>
        /// Get all todo items
        /// </summary>
        /// <returns>List of all todo items</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos()
        {
            try
            {
                var todos = await _todoService.GetAllTodosAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all todos");
                return StatusCode(500, "An error occurred while retrieving todos");
            }
        }
        
        /// <summary>
        /// Get a specific todo item by ID
        /// </summary>
        /// <param name="id">The ID of the todo item</param>
        /// <returns>The todo item if found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoById(int id)
        {
            try
            {
                var todo = await _todoService.GetTodoByIdAsync(id);
                
                if (todo == null)
                {
                    return NotFound($"Todo item with ID {id} not found");
                }
                
                return Ok(todo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting todo with ID: {TodoId}", id);
                return StatusCode(500, "An error occurred while retrieving the todo");
            }
        }
        
        /// <summary>
        /// Create a new todo item
        /// </summary>
        /// <param name="createTodoDto">The data for creating the todo item</param>
        /// <returns>The created todo item</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                if (string.IsNullOrWhiteSpace(createTodoDto.Title))
                {
                    return BadRequest("Title is required");
                }
                
                var createdTodo = await _todoService.CreateTodoAsync(createTodoDto);
                
                return CreatedAtAction(
                    nameof(GetTodoById),
                    new { id = createdTodo.Id },
                    createdTodo
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating todo");
                return StatusCode(500, "An error occurred while creating the todo");
            }
        }
        
        /// <summary>
        /// Update an existing todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to update</param>
        /// <param name="updateTodoDto">The data for updating the todo item</param>
        /// <returns>The updated todo item</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var updatedTodo = await _todoService.UpdateTodoAsync(id, updateTodoDto);
                
                if (updatedTodo == null)
                {
                    return NotFound($"Todo item with ID {id} not found");
                }
                
                return Ok(updatedTodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating todo with ID: {TodoId}", id);
                return StatusCode(500, "An error occurred while updating the todo");
            }
        }
        
        /// <summary>
        /// Delete a todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            try
            {
                var deleted = await _todoService.DeleteTodoAsync(id);
                
                if (!deleted)
                {
                    return NotFound($"Todo item with ID {id} not found");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting todo with ID: {TodoId}", id);
                return StatusCode(500, "An error occurred while deleting the todo");
            }
        }
        
        /// <summary>
        /// Toggle the completion status of a todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to toggle</param>
        /// <returns>The updated todo item</returns>
        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult<TodoItem>> ToggleTodoCompletion(int id)
        {
            try
            {
                var updatedTodo = await _todoService.ToggleTodoCompletionAsync(id);
                
                if (updatedTodo == null)
                {
                    return NotFound($"Todo item with ID {id} not found");
                }
                
                return Ok(updatedTodo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while toggling todo completion with ID: {TodoId}", id);
                return StatusCode(500, "An error occurred while toggling the todo completion");
            }
        }
    }
}
