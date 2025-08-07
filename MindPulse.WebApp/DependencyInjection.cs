using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindPulse.Infrastructure.Shared.Services;
using MindPulse.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Http;
using MindPulse.Infrastructure.Shared.Services.Analysis;


namespace MindPulse.WebApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMindPulseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IOpenAiService, OpenAiService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}