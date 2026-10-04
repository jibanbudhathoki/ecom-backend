using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Domain.Entities.Catalog;

namespace Ecom.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Products.AnyAsync(p => p.Slug == request.Slug, cancellationToken))
            {
                return Result.Failure(new[] { "A product with the specified slug already exists." });
            }

            var entity = new Product
            {
                Name = request.Name,
                Slug = request.Slug,
                ShortDescription = request.ShortDescription,
                Description = request.Description,
                Price = request.Price,
                OldPrice = request.OldPrice,
                CostPrice = request.CostPrice,
                StockQuantity = request.StockQuantity,
                IsActive = request.IsActive,
                IsFeatured = request.IsFeatured,
                ManageStock = request.ManageStock,
                SKU = request.SKU,
                Barcode = request.Barcode,
                ImageUrl = request.ImageUrl,
                MetaTitle = string.IsNullOrWhiteSpace(request.MetaTitle) ? request.Name : request.MetaTitle,
                MetaDescription = string.IsNullOrWhiteSpace(request.MetaDescription) ? request.ShortDescription ?? request.Description : request.MetaDescription,
                CategoryId = request.CategoryId,
                BrandId = request.BrandId,
                Created = DateTime.UtcNow,
                Images = request.Images ?? new(),
                Highlights = request.Highlights ?? new(),
                Specifications = request.Specifications ?? new()
            };

            foreach (var variantRequest in request.Variants)
            {
                entity.Variants.Add(new ProductVariant
                {
                    Name = variantRequest.Name,
                    SKU = variantRequest.SKU,
                    Price = variantRequest.Price,
                    StockQuantity = variantRequest.StockQuantity,
                    IsActive = variantRequest.IsActive,
                    OptionName = variantRequest.OptionName,
                    OptionValue = variantRequest.OptionValue,
                    Images = variantRequest.Images ?? new(),
                    Created = DateTime.UtcNow
                });
            }

            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
