using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Mappings;
<<<<<<< HEAD
using MindPulse.Core.Domain.Entities.Categories;
=======
using MindPulse.Core.Application.Services;
>>>>>>> master
using MindPulse.Core.Domain.Settings;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Persistence.Repositories;
using MindPulse.Infrastructure.Shared;
using MindPulse.Infrastructure.Shared.Services;
using MindPulse.WebApp;
<<<<<<< HEAD
using MindPulse.Core.Application.Services;
using MindPulse.Infrastructure.Persistence.Services;



=======
using System.Text;
using System.Text.Json.Serialization;
>>>>>>> master

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

<<<<<<< HEAD
// Registrar servicios de infraestructura y repositorios
=======

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionaireRepository, QuestionnaireRepository>();



// Registrar servicios específicos manualmente
>>>>>>> master
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IEmailService, EmailService>();
<<<<<<< HEAD
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
=======


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

>>>>>>> master

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
