using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Extensions;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(search) || p.SKU.ToLower().Contains(search));
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            if (request.BrandId.HasValue)
            {
                query = query.Where(p => p.BrandId == request.BrandId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                query = request.SortBy.ToLower() switch
                {
                    "name" => request.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => request.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "stock" => request.SortDescending ? query.OrderByDescending(p => p.StockQuantity) : query.OrderBy(p => p.StockQuantity),
                    _ => request.SortDescending ? query.OrderByDescending(p => p.Created) : query.OrderBy(p => p.Created)
                };
            }
            else
            {
                query = query.OrderByDescending(p => p.Created);
            }

            return await query.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    ShortDescription = p.ShortDescription,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    ImageUrl = p.ImageUrl,
                    IsActive = p.IsActive,
                    IsFeatured = p.IsFeatured,
                    SKU = p.SKU,
                    StockQuantity = p.StockQuantity,
                    VariantCount = p.Variants.Count,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    BrandName = p.Brand != null ? p.Brand.Name : null
                })
                .ToPagedResultAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
