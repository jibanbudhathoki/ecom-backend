using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Extensions;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, PagedResult<BrandDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBrandsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Brands.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.ToLower();
                query = query.Where(b => b.Name.ToLower().Contains(search) || b.Slug.ToLower().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "name" => request.SortDescending ? query.OrderByDescending(b => b.Name) : query.OrderBy(b => b.Name),
                    _ => request.SortDescending ? query.OrderByDescending(b => b.Id) : query.OrderBy(b => b.Id)
                };
            }
            else
            {
                query = query.OrderBy(b => b.Name);
            }

            return await query.Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Slug = b.Slug,
                    Description = b.Description,
                })
                .ToPagedResultAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
