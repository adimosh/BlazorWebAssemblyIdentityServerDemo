using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BlazorWebAssemblyIdentityServer.Client.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static bool InterpretStatusReturn(this HttpResponseMessage message, NavigationManager navigation, NotificationService notifications)
        {
            if (message.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Not logged in, redirect to login screen
                navigation.GoThroughLogin();

                return false;
            }

            if (message.StatusCode == HttpStatusCode.Forbidden)
            {
                // Forbidden, let's notify
                notifications.Notify(NotificationSeverity.Error, "Forbidden!", "Your user does not have the required rights to perform this action!");

                return false;
            }

            if (message.StatusCode == HttpStatusCode.Conflict)
            {
                // There was a conflict in the DB
                notifications.Notify(NotificationSeverity.Error, "Conflict!", "Someone else was editing the same data at the same time.");

                return false;
            }

            return true;
        }
    }
}
