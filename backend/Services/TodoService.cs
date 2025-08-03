using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Interfaces;
using TodoApp.Api.Models;

namespace TodoApp.Api.Services
{
    /// <summary>
    /// Service implementation for Todo operations
    /// Handles all business logic related to todo items
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoService> _logger;
        
        public TodoService(TodoDbContext context, ILogger<TodoService> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all todo items");
                return await _context.TodoItems
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all todos");
                throw;
            }
        }
        
        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving todo item with ID: {TodoId}", id);
                return await _context.TodoItems.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving todo with ID: {TodoId}", id);
                throw;
            }
        }
        
        public async Task<TodoItem> CreateTodoAsync(CreateTodoDto createTodoDto)
        {
            try
            {
                _logger.LogInformation("Creating new todo item: {Title}", createTodoDto.Title);
                
                var todoItem = new TodoItem
                {
                    Title = createTodoDto.Title,
                    Description = createTodoDto.Description,
                    Priority = createTodoDto.Priority,
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow
                };
                
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully created todo item with ID: {TodoId}", todoItem.Id);
                return todoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating todo item");
                throw;
            }
        }
        
        public async Task<TodoItem?> UpdateTodoAsync(int id, UpdateTodoDto updateTodoDto)
        {
            try
            {
                _logger.LogInformation("Updating todo item with ID: {TodoId}", id);
                
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    _logger.LogWarning("Todo item with ID: {TodoId} not found", id);
                    return null;
                }
                
                // Update only provided fields
                if (!string.IsNullOrEmpty(updateTodoDto.Title))
                {
                    todoItem.Title = updateTodoDto.Title;
                }
                
                if (updateTodoDto.Description != null)
                {
                    todoItem.Description = updateTodoDto.Description;
                }
                
                if (updateTodoDto.Priority.HasValue)
                {
                    todoItem.Priority = updateTodoDto.Priority.Value;
                }
                
                if (updateTodoDto.IsCompleted.HasValue)
                {
                    todoItem.IsCompleted = updateTodoDto.IsCompleted.Value;
                    todoItem.CompletedAt = updateTodoDto.IsCompleted.Value ? DateTime.UtcNow : null;
                }
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully updated todo item with ID: {TodoId}", id);
                return todoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating todo with ID: {TodoId}", id);
                throw;
            }
        }
        
        public async Task<bool> DeleteTodoAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting todo item with ID: {TodoId}", id);
                
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    _logger.LogWarning("Todo item with ID: {TodoId} not found", id);
                    return false;
                }
                
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully deleted todo item with ID: {TodoId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting todo with ID: {TodoId}", id);
                throw;
            }
        }
        
        public async Task<TodoItem?> ToggleTodoCompletionAsync(int id)
        {
            try
            {
                _logger.LogInformation("Toggling completion status for todo item with ID: {TodoId}", id);
                
                var todoItem = await _context.TodoItems.FindAsync(id);
                if (todoItem == null)
                {
                    _logger.LogWarning("Todo item with ID: {TodoId} not found", id);
                    return null;
                }
                
                todoItem.IsCompleted = !todoItem.IsCompleted;
                todoItem.CompletedAt = todoItem.IsCompleted ? DateTime.UtcNow : null;
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("Successfully toggled completion status for todo item with ID: {TodoId}", id);
                return todoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while toggling completion for todo with ID: {TodoId}", id);
                throw;
            }
        }
    }
}
