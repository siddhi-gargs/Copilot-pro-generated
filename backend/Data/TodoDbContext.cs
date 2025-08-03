using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Models;

namespace TodoApp.Api.Data
{
    /// <summary>
    /// Database context for the Todo application
    /// Using Entity Framework Core with In-Memory database for simplicity
    /// </summary>
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
        
        public DbSet<TodoItem> TodoItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure TodoItem entity
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Description)
                    .HasMaxLength(1000);
                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });
            
            // Seed some initial data
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = 1,
                    Title = "Welcome to Todo App",
                    Description = "This is a sample todo item. You can edit or delete it!",
                    IsCompleted = false,
                    Priority = TodoPriority.Medium,
                    CreatedAt = DateTime.UtcNow
                },
                new TodoItem
                {
                    Id = 2,
                    Title = "Learn React",
                    Description = "Complete the React tutorial",
                    IsCompleted = true,
                    Priority = TodoPriority.High,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    CompletedAt = DateTime.UtcNow
                }
            );
        }
    }
}
