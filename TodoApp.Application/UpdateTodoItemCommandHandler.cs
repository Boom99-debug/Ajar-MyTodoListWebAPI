using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Application.Handlers
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public UpdateTodoItemCommandHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _repository.GetByIdAsync(request.Id);
            if (existingItem == null)
            {
                return null; // Or throw a specific exception
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("To-do item content cannot be empty.");
            }

            existingItem.Title = request.Title;
            existingItem.IsCompleted = request.IsCompleted;

            await _repository.UpdateAsync(existingItem);
            return existingItem;
        }
    }
}