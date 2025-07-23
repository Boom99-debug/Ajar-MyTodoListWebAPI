using MediatR;
using TodoApp.Application.Queries;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Application.Handlers
{
    public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, IEnumerable<TodoItem>>
    {
        private readonly ITodoItemRepository _repository;

        public GetAllTodoItemsQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}