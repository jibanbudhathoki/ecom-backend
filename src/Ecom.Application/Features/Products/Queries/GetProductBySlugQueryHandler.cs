
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductDetailDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetProductBySlugQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDetailDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Variants)
                .Where(p => p.Slug == request.Slug)
                .Select(p => new ProductDetailDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    ShortDescription = p.ShortDescription,
                    Description = p.Description,
                    Price = p.Price,
                    OldPrice = p.OldPrice,
                    CostPrice = p.CostPrice,
                    StockQuantity = p.StockQuantity,
                    IsActive = p.IsActive,
                    IsFeatured = p.IsFeatured,
                    ManageStock = p.ManageStock,
                    SKU = p.SKU,
                    Barcode = p.Barcode,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    BrandId = p.BrandId,
                    BrandName = p.Brand != null ? p.Brand.Name : null,
                    Variants = p.Variants.Select(v => new ProductVariantDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        SKU = v.SKU,
                        Price = v.Price,
                        StockQuantity = v.StockQuantity,
                        IsActive = v.IsActive
                    }).ToList(),
                    CreatedAt = p.Created,
                    UpdatedAt = p.LastModified
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
