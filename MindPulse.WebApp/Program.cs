using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Mappings;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Settings;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Persistence.Repositories;
using MindPulse.Infrastructure.Shared;
using MindPulse.Infrastructure.Shared.Services;
using MindPulse.WebApp;
using MindPulse.Core.Application.Services;
using MindPulse.Infrastructure.Persistence.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Registrar HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Registrar AutoMapper con perfil DefaultProfile
builder.Services.AddAutoMapper(typeof(DefaultProfile));

// Registrar servicios de infraestructura y repositorios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Servicios compartidos
builder.Services.AddMindPulseDependencies(builder.Configuration);

// Controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
