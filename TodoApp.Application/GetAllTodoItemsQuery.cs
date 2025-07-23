using MediatR;
using TodoApp.Core.Entities;
using System.Collections.Generic;

namespace TodoApp.Application.Queries
{
    public class GetAllTodoItemsQuery : IRequest<IEnumerable<TodoItem>> { }
}