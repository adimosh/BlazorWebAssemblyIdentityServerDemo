using BlazorWebAssemblyIdentityServer.WebApp.Data;
using BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebAssemblyIdentityServer.WebApp
{
    /// <summary>
    /// This class is used solely for the purpose of &quot;dotnet ef&quot; core tools.
    /// </summary>
    public class SimpleStartup
    {
        public IConfiguration Configuration { get; }

        public SimpleStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.SetDbContext();
        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}