using Microsoft.EntityFrameworkCore;
using TodoListApp.Domain.Repositories;
using TodoListApp.Infrastructure.Data;
using TodoListApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = "Data Source=../todos.db";
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
