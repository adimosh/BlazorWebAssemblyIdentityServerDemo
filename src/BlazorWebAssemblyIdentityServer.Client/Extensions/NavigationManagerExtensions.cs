using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityServer.Client.Extensions
{
    internal static class NavigationManagerExtensions
    {
        internal static void GoThroughLogin(this NavigationManager navigation)
        {
            var returnUrl = navigation.ToBaseRelativePath(navigation.Uri);

            navigation.NavigateTo(
                string.IsNullOrWhiteSpace(returnUrl)
                    ? "authentication/login"
                    : $"authentication/login?returnUrl={Url.Encode(returnUrl)}",
                true);
        }
    }
}
