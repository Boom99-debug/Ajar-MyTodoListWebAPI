using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TodoApp.Application.Handlers;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp API", Version = "v1" });
});

// Configure Database (Using In-Memory for simplicity, switch to SQLite for persistence)
// For In-Memory:
builder.Services.AddDbContext<TodoAppDbContext>(options =>
    options.UseInMemoryDatabase("TodoDb"));

// For SQLite: Uncomment the following and remove the In-Memory line.
// Ensure you have a "Todo.db" file or it will be created.
// builder.Services.AddDbContext<TodoAppDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register repositories
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTodoItemCommandHandler).Assembly));


// Add CORS policy (important for React frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Replace with your React app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp API V1");
    });
}

//app.UseHttpsRedirection();

app.UseRouting(); // Important for CORS and endpoint routing

app.UseCors("AllowReactApp"); // Apply the CORS policy

app.UseAuthorization();

app.MapControllers();

// Ensure database is created/migrated for SQLite (not needed for In-Memory as it's created on startup)
// if (app.Environment.IsDevelopment())
// {
//     using (var scope = app.Services.CreateScope())
//     {
//         var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();
//         dbContext.Database.EnsureCreated(); // Or dbContext.Database.Migrate(); for migrations
//     }
// }


app.Run();