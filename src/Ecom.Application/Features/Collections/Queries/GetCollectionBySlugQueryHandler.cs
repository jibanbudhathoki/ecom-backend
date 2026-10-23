using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Features.Products.Queries;

namespace Ecom.Application.Features.Collections.Queries
{
    public class GetCollectionBySlugQueryHandler : IRequestHandler<GetCollectionBySlugQuery, CollectionDetailDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCollectionBySlugQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CollectionDetailDto?> Handle(GetCollectionBySlugQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Collections
                .AsNoTracking()
                .Include(c => c.ProductCollections)
                    .ThenInclude(pc => pc.Product)
                        .ThenInclude(p => p.Category)
                .Include(c => c.ProductCollections)
                    .ThenInclude(pc => pc.Product)
                        .ThenInclude(p => p.Brand)
                .Include(c => c.ProductCollections)
                    .ThenInclude(pc => pc.Product)
                        .ThenInclude(p => p.Variants)
                .FirstOrDefaultAsync(c => c.Slug == request.Slug, cancellationToken);

            if (entity == null) return null;

            return new CollectionDetailDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Type = entity.Type,
                ConditionsJson = entity.ConditionsJson,
                IsActive = entity.IsActive,
                ProductsCount = entity.ProductCollections.Count,
                Products = entity.ProductCollections
                    .OrderBy(pc => pc.SortOrder)
                    .Where(pc => pc.Product != null)
                    .Select(pc => new ProductDto
                    {
                        Id = pc.Product.Id,
                        Name = pc.Product.Name,
                        Slug = pc.Product.Slug,
                        ShortDescription = pc.Product.ShortDescription,
                        Price = pc.Product.Price,
                        OldPrice = pc.Product.OldPrice,
                        ImageUrl = pc.Product.ImageUrl,
                        IsActive = pc.Product.IsActive,
                        IsFeatured = pc.Product.IsFeatured,
                        SKU = pc.Product.SKU,
                        StockQuantity = pc.Product.StockQuantity,
                        VariantCount = pc.Product.Variants.Count,
                        CategoryId = pc.Product.CategoryId,
                        CategoryName = pc.Product.Category != null ? pc.Product.Category.Name : null,
                        BrandName = pc.Product.Brand != null ? pc.Product.Brand.Name : null
                    })
                    .ToList()
            };
        }
    }
}
