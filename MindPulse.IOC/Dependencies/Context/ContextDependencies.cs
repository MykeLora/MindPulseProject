using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MindPulse.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindPulse.IOC.Dependencies.Context
{
    public static class ContextDependencies
    {

        public static void AddContextDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
