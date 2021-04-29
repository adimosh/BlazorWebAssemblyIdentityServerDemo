using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityServer.WebApp.Extensions
{
    internal static class IQueryableExtensions
    {
        internal static IQueryable<TEntity> UseQueryElements<TEntity>(
            this IQueryable<TEntity> source,
            int? skip = null,
            int? top = null,
            string orderBy = null,
            string filter = null)
            where TEntity : class
        {
            if (skip is > 0)
            {
                int value = skip.Value;
                source = source.Skip(value);
            }

            if (top is > 0)
            {
                int value = top.Value;
                source = source.Take(value);
            }

            if (orderBy is not null or "")
            {
                source = source.OrderBy(orderBy);
            }

            if (filter is not null or "")
            {
                source = source.Where(filter);
            }

            return source;
        }
    }
}