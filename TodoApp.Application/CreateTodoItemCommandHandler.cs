using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Application.Handlers
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public CreateTodoItemCommandHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("To-do item content cannot be empty.");
            }

            var todoItem = new TodoItem { Title = request.Title, IsCompleted = false };
            await _repository.AddAsync(todoItem);
            return todoItem;
        }
    }
}