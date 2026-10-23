using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Extensions;

namespace Ecom.Application.Features.Collections.Queries
{
    public class GetCollectionsQueryHandler : IRequestHandler<GetCollectionsQuery, PagedResult<CollectionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCollectionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CollectionDto>> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Collections.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(search) || c.Slug.ToLower().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(request.Type))
            {
                query = query.Where(c => c.Type == request.Type);
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == request.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "name" => request.SortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                    "created" => request.SortDescending ? query.OrderByDescending(c => c.Created) : query.OrderBy(c => c.Created),
                    _ => request.SortDescending ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id)
                };
            }
            else
            {
                query = query.OrderByDescending(c => c.Created);
            }

            return await query.Select(c => new CollectionDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Type = c.Type,
                    ConditionsJson = c.ConditionsJson,
                    IsActive = c.IsActive,
                    MetaTitle = c.MetaTitle,
                    MetaDescription = c.MetaDescription,
                    ProductsCount = c.ProductCollections.Count
                })
                .ToPagedResultAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
