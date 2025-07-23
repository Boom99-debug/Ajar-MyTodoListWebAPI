using MediatR;
using TodoApp.Application.Queries;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Application.Handlers
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public GetTodoItemByIdQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}