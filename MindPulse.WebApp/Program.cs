using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Mappings;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Settings;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Persistence.Repositories;
using MindPulse.Infrastructure.Shared;
using MindPulse.Infrastructure.Shared.Services;
using MindPulse.WebApp;
using System.Text;
using System.Text.Json.Serialization;
using MindPulse.Infrastructure.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Service Email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


// Registrar HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Registrar AutoMapper con el perfil DefaultProfile
builder.Services.AddAutoMapper(typeof(DefaultProfile));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();




// Registrar servicios específicos manualmente
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


// Registrar servicios compartidos (como los definidos en Shared)
builder.Services.AddMindPulseDependencies(builder.Configuration);

builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar middleware HTTP
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
