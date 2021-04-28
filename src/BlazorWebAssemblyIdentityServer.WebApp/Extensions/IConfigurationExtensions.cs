using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlazorWebAssemblyIdentityServer.WebApp.Extensions
{
    public static class IConfigurationExtensions
    {
        public static string GetConfiguredEndpoint(this IConfiguration configuration)
        {
            string url = configuration["profiles:applicationUrl"];
            if (url is null or "")
            {
                url = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
            }

            if (url is null or "")
            {
                url = Environment.GetEnvironmentVariable("DOTNET_URLS");
            }

            if (url is null or "")
            {
                return string.Empty;
            }

            var urls = url.Split(';');

            return urls.FirstOrDefault(
                       p => Flurl.Url.IsValid(p) &&
                       p.StartsWith(
                           "https://",
                           StringComparison.OrdinalIgnoreCase)) ??
                   urls.FirstOrDefault(
                       p => Flurl.Url.IsValid(p) &&
                       p.StartsWith(
                           "http://",
                           StringComparison.OrdinalIgnoreCase)) ??
                   urls.FirstOrDefault(Flurl.Url.IsValid) ?? string.Empty;
        }
    }
}
