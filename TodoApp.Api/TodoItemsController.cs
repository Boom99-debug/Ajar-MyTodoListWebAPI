using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Commands;
using TodoApp.Application.Queries;
using TodoApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var query = new GetAllTodoItemsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var query = new GetTodoItemByIdQuery { Id = id };
            var todoItem = await _mediator.Send(query);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CreateTodoItemCommand command)
        {
            try
            {
                var todoItem = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedTodoItem = await _mediator.Send(command);
                if (updatedTodoItem == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var command = new DeleteTodoItemCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}