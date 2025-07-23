using MediatR;
using TodoApp.Core.Entities;

namespace TodoApp.Application.Commands
{
    public class CreateTodoItemCommand : IRequest<TodoItem>
    {
        public string Title { get; set; }
    }
}