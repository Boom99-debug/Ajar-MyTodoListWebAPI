using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Application.Handlers
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, bool>
    {
        private readonly ITodoItemRepository _repository;

        public DeleteTodoItemCommandHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _repository.GetByIdAsync(request.Id);
            if (existingItem == null)
            {
                return false;
            }
            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}