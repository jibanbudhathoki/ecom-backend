using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecom.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query, 
            int pageNumber, 
            int pageSize, 
            CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
