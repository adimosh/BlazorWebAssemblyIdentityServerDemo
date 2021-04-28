using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebAssemblyIdentityServer.WebApp.Data
{
    internal static class ServicesDataContextExtensions
    {
        internal static void SetDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext, NpgsqlApplicationDbContext>(ServiceLifetime.Transient);
            services.AddDbContext<NpgsqlApplicationDbContext>(ServiceLifetime.Transient);
        }

        internal static ApplicationDbContext GetDbContext(this IServiceProvider services)
        {
            return services.GetService<ApplicationDbContext>();
        }
    }
}
