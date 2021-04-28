using BlazorWebAssemblyIdentityServer.WebApp.Data;
using BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebAssemblyIdentityServer.WebApp
{
    public class SimpleStartup
    {
        public IConfiguration Configuration { get; }

        public SimpleStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext, NpgsqlApplicationDbContext>(ServiceLifetime.Transient);
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}