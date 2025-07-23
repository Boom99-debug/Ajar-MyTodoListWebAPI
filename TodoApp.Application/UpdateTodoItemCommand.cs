using MediatR;
using TodoApp.Core.Entities;

namespace TodoApp.Application.Commands
{
    public class UpdateTodoItemCommand : IRequest<TodoItem>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}