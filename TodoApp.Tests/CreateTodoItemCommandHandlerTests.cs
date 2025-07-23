using Moq; // For mocking
using TodoApp.Application.Commands; // Reference to the command we're testing
using TodoApp.Application.Handlers; // Reference to the handler we're testing
using TodoApp.Core.Entities; // Reference to the domain entity
using TodoApp.Core.Interfaces; // Reference to the interface we're mocking
using Xunit; // xUnit framework attributes
using System.Threading; // For CancellationToken
using System.Threading.Tasks; // For async/await
using System; // For ArgumentException
using TodoApp.Application;

namespace TodoApp.Tests.Application
{
    public class CreateTodoItemCommandHandlerTests
    {
        [Fact] // Denotes a single unit test method
        public async Task Handle_ValidCommand_AddsTodoItem()
        {
            // ARRANGE
            // 1. Mock the repository dependency
            var mockRepo = new Mock<ITodoItemRepository>();
            // We don't need to specify what AddAsync does, as we'll verify it's called.
            // mockRepo.Setup(r => r.AddAsync(It.IsAny<TodoItem>())).Returns(Task.CompletedTask); // Optional explicit setup

            // 2. Instantiate the handler (the unit under test) with the mocked dependency
            var handler = new CreateTodoItemCommandHandler(mockRepo.Object);

            // 3. Create the command input
            var command = new CreateTodoItemCommand { Title = "Test Todo Item" };

            // ACT
            // Execute the handler's business logic
            var result = await handler.Handle(command, CancellationToken.None);

            // ASSERT
            // 1. Verify the return value
            Assert.NotNull(result);
            Assert.Equal("Test Todo Item", result.Title);
            Assert.False(result.IsCompleted);
            Assert.True(result.Id == 0); // Id is usually 0 before being saved to a real DB, then gets assigned by EF Core.

            // 2. Verify that the mocked repository's AddAsync method was called exactly once
            //    with any TodoItem object.
            mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyTitle_ThrowsArgumentException()
        {
            // ARRANGE
            var mockRepo = new Mock<ITodoItemRepository>();
            var handler = new CreateTodoItemCommandHandler(mockRepo.Object);
            var command = new CreateTodoItemCommand { Title = "" }; // Empty title

            // ACT & ASSERT
            // Use Assert.ThrowsAsync to check if a specific exception is thrown
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));

            // Verify that the AddAsync method on the repository was *never* called,
            // because the validation failed before it reached the repository.
            mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Never);
        }

        [Fact]
        public async Task Handle_WhitespaceTitle_ThrowsArgumentException()
        {
            // ARRANGE
            var mockRepo = new Mock<ITodoItemRepository>();
            var handler = new CreateTodoItemCommandHandler(mockRepo.Object);
            var command = new CreateTodoItemCommand { Title = "   " }; // Whitespace title

            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));

            mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>()), Times.Never);
        }

        // --- Add more tests for other handlers ---

        // Example for GetAllTodoItemsQueryHandler (assuming you want to test this too)
        [Fact]
        public async Task GetAllTodoItemsQueryHandler_ReturnsAllItems()
        {
            // ARRANGE
            var mockRepo = new Mock<ITodoItemRepository>();
            var expectedItems = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test 1" },
                new TodoItem { Id = 2, Title = "Test 2" }
            };
            // Setup the mock repository to return our predefined list when GetAllAsync is called
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedItems);

            var handler = new GetAllTodoItemsQueryHandler(mockRepo.Object);
            var query = new TodoApp.Application.Queries.GetAllTodoItemsQuery();

            // ACT
            var result = await handler.Handle(query, CancellationToken.None);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // Check the count of returned items
            Assert.Contains(result, item => item.Title == "Test 1"); // Check for specific items
            mockRepo.Verify(r => r.GetAllAsync(), Times.Once); // Verify the repository method was called
        }
    }
}