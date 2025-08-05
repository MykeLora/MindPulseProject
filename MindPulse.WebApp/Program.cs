using Microsoft.AspNetCore.Authentication.JwtBearer;
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

// ------------------------------
// 🔗 DATABASE
// ------------------------------
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ------------------------------
// 📧 EMAIL CONFIG
// ------------------------------
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// ------------------------------
// 🧭 UTILS
// ------------------------------
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(DefaultProfile));

// ------------------------------
// 📦 REPOSITORIES
// ------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<IUserResponseRepository, UserResponseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IEducationalContentRepository, EducationalContentRepository>();
builder.Services.AddScoped<IAiResponseRepository, AiResponseRepository>();
builder.Services.AddScoped<IAnswerOptionRepository, AnswerOptionRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();

// ------------------------------
// 💼 SERVICES
// ------------------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFreeTextOrchestrationService, FreeTextOrchestrationService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionnaireService, QuestionnaireService>();
builder.Services.AddScoped<IUserResponseService, UserResponseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IEducationalContentService, EducationalContentService>();
builder.Services.AddScoped<IAiResponseService, AiResponseService>();
builder.Services.AddScoped<IAnswerOptionService, AnswerOptionService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestResultService, TestResultService>();

// ------------------------------
// 🤝 SHARED DEPENDENCIES
// ------------------------------
builder.Services.AddMindPulseDependencies(builder.Configuration);

// ------------------------------
// 🎯 JSON OPTIONS
// ------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });



// ------------------------------
// 🔐 JWT AUTHENTICATION
// ------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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

// ------------------------------
// 🛡️ ROLE-BASED AUTHORIZATION
// ------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

// ------------------------------
// 🧪 SWAGGER
// ------------------------------
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MindPulse API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT con el formato: Bearer {tu_token}"
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
            Array.Empty<string>()
        }
    });
});

// ------------------------------
// 🚀 BUILD APP
// ------------------------------
var app = builder.Build();

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
