using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindPulse.Core.Application.Interfaces.Repositories;
using MindPulse.Core.Application.Interfaces.Repositories.Recommendations;
using MindPulse.Core.Application.Interfaces.Services;
using MindPulse.Core.Application.Interfaces.Services.Recommendations;
using MindPulse.Core.Application.Mappings;
using MindPulse.Core.Application.Services;
using MindPulse.Core.Application.Services.Recommendations;
using MindPulse.Core.Domain.Entities.Categories;
using MindPulse.Core.Domain.Settings;
using MindPulse.Infrastructure.Persistence.Context;
using MindPulse.Infrastructure.Persistence.Repositories;
using MindPulse.Infrastructure.Persistence.Repositories.Recommendations;
using MindPulse.Infrastructure.Persistence.Services;
using MindPulse.Infrastructure.Shared;
using MindPulse.Infrastructure.Shared.Services;
using MindPulse.WebApp;
using System.Text;
using System.Text.Json.Serialization;


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

// Registrar Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IUserResponseRepository, UserResponseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IEducationalContentRepository, EducationalContentRepository>();

// Registrar servicios específicos manualmente
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserResponseService, UserResponseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IEducationalContentService, EducationalContentService>();

// Registrar servicios de infraestructura
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


// Registrar servicios compartidos (como los definidos en Shared)
builder.Services.AddMindPulseDependencies(builder.Configuration);

builder.Services.AddControllers();

// Configurar autenticación JWT
builder.Services.AddAuthentication("JwtBearer")
    .AddJwtBearer("JwtBearer", options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Configurar Swagger/OpenAPI con autenticación
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MindPulse API",
        Version = "v1"
    });

    // Configuración para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT con el formato: bearer {tu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            new string[] {}
        }
    });
});


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
