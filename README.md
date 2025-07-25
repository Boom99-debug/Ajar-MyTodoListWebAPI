To-Do List Web API with .NET and React 



&nbsp;	Project Overview:

&nbsp;	This project implements a comprehensive To-Do List application. It consists of a robust RESTful Web API built with .NET 8/9 (using Clean Architecture principles) and a responsive 	frontend UI developed with React.js. The solution demonstrates best practices in software development, including separation of concerns, testability, and clear API documentation.



&nbsp;	Features:

&nbsp;	Backend API (.NET)

&nbsp;	RESTful Endpoints: Provides standard CRUD (Create, Read, Update, Delete) operations for managing to-do items:

&nbsp;	GET /api/TodoItems: Retrieve all to-do items.

&nbsp;	GET /api/TodoItems/{id}: Retrieve a single to-do item by ID.

&nbsp;	POST /api/TodoItems: Create a new to-do item.

&nbsp;	PUT /api/TodoItems/{id}: Update an existing to-do item (title and completion status).

&nbsp;	DELETE /api/TodoItems/{id}: Delete a to-do item by ID.

&nbsp;	Clean Architecture: The solution is structured into distinct layers (Core, Application, Infrastructure, Api) to ensure high cohesion, low coupling, and maintainability.

&nbsp;	TodoApp.Core: Contains domain entities (TodoItem) and core interfaces (ITodoItemRepository).

&nbsp;	TodoApp.Application: Houses application-specific logic, commands, queries, and their handlers (using MediatR).

&nbsp;	TodoApp.Infrastructure: Implements data access (using Entity Framework Core with an In-Memory database) and concrete repository implementations.

&nbsp;	TodoApp.Api: The entry point, exposing controllers, configuring dependency injection, and setting up middleware.

&nbsp;	Data Storage: Utilizes an In-Memory Database for simplicity and ease of setup during development. This can be easily swapped for a persistent database like SQLite, SQL Server, or 	PostgreSQL.

&nbsp;	Input Validation: Basic validation ensures that to-do item titles cannot be empty.

&nbsp;	Swagger/OpenAPI Documentation: Provides an interactive API documentation interface via Swagger UI, allowing developers to explore and test endpoints directly.

&nbsp;	Unit Tests: Covers core business logic within the Application layer using xUnit and Moq to ensure reliability and correctness.

&nbsp;	

&nbsp;	Frontend UI (React)

&nbsp;	To-Do Management:

&nbsp;	Displays a list of all to-do items fetched from the backend API.

&nbsp;	Allows users to add new to-do items.

&nbsp;	Supports editing the title of existing to-do items.

&nbsp;	Enables toggling the completion status of to-do items.

&nbsp;	Provides functionality to delete to-do items.

&nbsp;	API Integration: Interacts seamlessly with the .NET Backend API using Axios for HTTP requests.

&nbsp;	User Experience: Features a simple, functional, and clean UI design for intuitive to-do management.

&nbsp;	Technologies Used

&nbsp;	Backend (.NET)

&nbsp;	.NET SDK: Version 8 or 9 (as per your csproj target framework)

&nbsp;	ASP.NET Core Web API: For building RESTful services.

&nbsp;	Entity Framework Core: ORM for data access.

&nbsp;	MediatR: A simple, unambitious mediator implementation for in-process messaging, promoting a CQRS-like pattern.

&nbsp;	Swashbuckle.AspNetCore: For generating Swagger/OpenAPI documentation.

&nbsp;	xUnit: A free, open-source, community-focused unit testing tool for .NET.

&nbsp;	Moq: A popular mocking library for .NET, used for isolating dependencies in unit tests.

&nbsp;	Frontend (React)

&nbsp;	React.js: A JavaScript library for building user interfaces.

&nbsp;	Node.js \& npm: JavaScript runtime and package manager for React development.

&nbsp;	Axios: A promise-based HTTP client for making API requests.

&nbsp;	HTML \& CSS: For structuring and styling the user interface.

&nbsp;		

&nbsp;	How to Run the Application:-

&nbsp;	1. Clone the Repository

&nbsp;	First, clone this repository to your local machine:

&nbsp;	git clone https://github.com/Boom99-debug/Ajar-MyTodoListWebAPI.git



&nbsp;	2. Run the Backend API (.NET)

&nbsp;	You can run the backend API either via the command line or directly through Visual Studio.

&nbsp;	Option A: Run Backend via Command Line

&nbsp;	1. Open a terminal or command prompt.

&nbsp;	2. Navigate to the TodoApp.Api project directory:

&nbsp;	cd TodoApp/TodoApp.Api

&nbsp;	3. Restore .NET dependencies (if not already done):

&nbsp;	dotnet restore

&nbsp;	4. Run the application:

&nbsp;	dotnet run

&nbsp;	

&nbsp;	The API should start and display "Now listening on:" messages, typically https://localhost:7173 and http://localhost:5093. Note the HTTPS port (e.g., 7173) as you'll need it for 	the frontend.

&nbsp;	You can verify the API is running by navigating to https://localhost:7173/swagger (replace 7173 with your actual port) in your browser.

&nbsp;	

&nbsp;	Option B: Run Backend via Visual Studio

&nbsp;	1. Open the TodoApp.sln solution in Visual Studio.

&nbsp;	2. In Solution Explorer, right-click on the TodoApp.Api project.

&nbsp;	3. Select Debug > Start New Instance or simply press F5 if TodoApp.Api is set as the single startup project.

&nbsp;	Visual Studio will start the API, and a console window will pop up showing the listening URLs (e.g., https://localhost:7173).

&nbsp;	3. Run the Frontend UI (React)

&nbsp;	Make sure your .NET Backend API is running before starting the React frontend.

&nbsp;	Run Frontend via Command Line

&nbsp;	1. Open a new, separate terminal or command prompt window.

&nbsp;	2. Navigate to the todoapp-frontend project directory:

&nbsp;	cd TodoApp/todoapp-frontend

&nbsp;	3. Install Node.js dependencies (first time only):

&nbsp;	npm install

&nbsp;	4. Start the React development server:

&nbsp;	npm start

&nbsp;	

&nbsp;	This will typically open your default web browser to http://localhost:3000.

&nbsp;	

&nbsp;	Unit Tests:-

&nbsp;	Unit tests for the core business logic are located in the TodoApp.Tests project.

&nbsp;	To run tests:

&nbsp;	1. Open a terminal or command prompt.

&nbsp;	2. Navigate to the solution root directory: cd TodoApp

&nbsp;	3. Execute: dotnet test

