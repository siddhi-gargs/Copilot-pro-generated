using TodoApp.Api.Models;

namespace TodoApp.Api.Interfaces
{
    /// <summary>
    /// Interface defining the contract for Todo service operations
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Retrieves all todo items
        /// </summary>
        /// <returns>List of all todo items</returns>
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        
        /// <summary>
        /// Retrieves a specific todo item by ID
        /// </summary>
        /// <param name="id">The ID of the todo item</param>
        /// <returns>The todo item if found, null otherwise</returns>
        Task<TodoItem?> GetTodoByIdAsync(int id);
        
        /// <summary>
        /// Creates a new todo item
        /// </summary>
        /// <param name="createTodoDto">The data for creating the todo item</param>
        /// <returns>The created todo item</returns>
        Task<TodoItem> CreateTodoAsync(CreateTodoDto createTodoDto);
        
        /// <summary>
        /// Updates an existing todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to update</param>
        /// <param name="updateTodoDto">The data for updating the todo item</param>
        /// <returns>The updated todo item if found, null otherwise</returns>
        Task<TodoItem?> UpdateTodoAsync(int id, UpdateTodoDto updateTodoDto);
        
        /// <summary>
        /// Deletes a todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to delete</param>
        /// <returns>True if deleted successfully, false if not found</returns>
        Task<bool> DeleteTodoAsync(int id);
        
        /// <summary>
        /// Toggles the completion status of a todo item
        /// </summary>
        /// <param name="id">The ID of the todo item</param>
        /// <returns>The updated todo item if found, null otherwise</returns>
        Task<TodoItem?> ToggleTodoCompletionAsync(int id);
    }
}
