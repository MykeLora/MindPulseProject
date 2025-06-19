using Microsoft.EntityFrameworkCore;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Shared;
using MindPulse.WebApp;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Infrastructure.Persistence.Repositories;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the OpenAI service with the dependency injection container.
builder.Services.AddMindPulseDependencies(builder.Configuration);
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
