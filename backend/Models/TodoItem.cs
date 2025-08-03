namespace TodoApp.Api.Models
{
    /// <summary>
    /// Represents a Todo item in the application
    /// </summary>
    public class TodoItem
    {
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? CompletedAt { get; set; }
        
        public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    }
    
    /// <summary>
    /// Enum representing the priority levels of a todo item
    /// </summary>
    public enum TodoPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
