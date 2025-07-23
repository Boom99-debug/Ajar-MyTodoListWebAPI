using MediatR;

namespace TodoApp.Application.Commands
{
    public class DeleteTodoItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}