using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityServer.Client.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static bool InterpretStatusReturn(this HttpResponseMessage message, NavigationManager navigation)
        {
            if (message.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Not logged in, redirect to login screen
                navigation.GoThroughLogin();

                return false;
            }

            if (message.StatusCode == HttpStatusCode.Forbidden)
            {
                return false;
            }

            return true;
        }
    }
}
