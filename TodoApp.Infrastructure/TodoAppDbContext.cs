using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.Infrastructure.Data
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(t => t.Title).IsRequired();
            // Seed some data for initial testing
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Learn Clean Architecture", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Build Todo API", IsCompleted = false },
                new TodoItem { Id = 3, Title = "Integrate React Frontend", IsCompleted = false }
            );
        }
    }
}