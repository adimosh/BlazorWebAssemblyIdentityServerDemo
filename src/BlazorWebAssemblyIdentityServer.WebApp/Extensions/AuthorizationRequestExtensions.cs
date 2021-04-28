using System;
using IdentityServer4.Models;

namespace BlazorWebAssemblyIdentityServer.WebApp.Extensions
{
    internal static class AuthorizationRequestExtensions
    {
        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <returns></returns>
        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            var redirectUri = context.RedirectUri;
            return !redirectUri.StartsWith("https", StringComparison.Ordinal)
                   && !redirectUri.StartsWith("http", StringComparison.Ordinal);
        }
    }
}
