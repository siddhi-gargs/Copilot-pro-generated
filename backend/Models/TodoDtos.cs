namespace TodoApp.Api.Models
{
    /// <summary>
    /// DTO for creating a new todo item
    /// </summary>
    public class CreateTodoDto
    {
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    }
    
    /// <summary>
    /// DTO for updating an existing todo item
    /// </summary>
    public class UpdateTodoDto
    {
        public string? Title { get; set; }
        
        public string? Description { get; set; }
        
        public bool? IsCompleted { get; set; }
        
        public TodoPriority? Priority { get; set; }
    }
}
