using Flurl;
using Radzen;

namespace BlazorWebAssemblyIdentityServer.Client.Extensions
{
    internal static class LoadDataArgsExtensions
    {
        internal static string AttachToUrl(
            this LoadDataArgs args,
            string url) =>
            url.SetQueryParam(
                    "skip",
                    args.Skip)
                .SetQueryParam(
                    "top",
                    args.Top)
                .SetQueryParam(
                    "orderBy",
                    args.OrderBy)
                .SetQueryParam(
                    "filter",
                    args.Filter);
    }
}
