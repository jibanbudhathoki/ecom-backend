using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ecom.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                return Result.Failure(new[] { "Product not found." });
            }

            product.Name = request.Name;
            product.Slug = request.Slug;
            product.ShortDescription = request.ShortDescription;
            product.Description = request.Description;
            product.Price = request.Price;
            product.OldPrice = request.OldPrice;
            product.CostPrice = request.CostPrice;
            product.StockQuantity = request.StockQuantity;
            product.IsActive = request.IsActive;
            product.IsFeatured = request.IsFeatured;
            product.ManageStock = request.ManageStock;
            product.SKU = request.SKU;
            product.Barcode = request.Barcode;
            
            if (!string.IsNullOrEmpty(request.ImageUrl))
            {
                product.ImageUrl = request.ImageUrl;
            }
            
            product.MetaTitle = request.MetaTitle;
            product.MetaDescription = request.MetaDescription;
            product.CategoryId = request.CategoryId;
            product.BrandId = request.BrandId;
            
            // Rich data
            if (request.Images != null && request.Images.Any())
            {
                product.Images = request.Images;
            }
            
            product.Specifications = request.Specifications ?? new();
            product.Highlights = request.Highlights ?? new();

            // Handle variants
            if (request.Variants != null)
            {
                // Remove variants not in the incoming list
                var incomingIds = request.Variants.Where(v => v.Id > 0).Select(v => v.Id).ToList();
                var toRemove = product.Variants.Where(v => !incomingIds.Contains(v.Id)).ToList();
                foreach (var rv in toRemove)
                {
                    product.Variants.Remove(rv);
                }

                foreach (var rv in request.Variants)
                {
                    if (rv.Id > 0)
                    {
                        var existing = product.Variants.FirstOrDefault(v => v.Id == rv.Id);
                        if (existing != null)
                        {
                            existing.Name = rv.Name;
                            existing.SKU = rv.SKU;
                            existing.Price = rv.Price;
                            existing.StockQuantity = rv.StockQuantity;
                            existing.IsActive = rv.IsActive;
                            existing.OptionName = rv.OptionName;
                            existing.OptionValue = rv.OptionValue;
                            if (rv.Images != null && rv.Images.Any())
                            {
                                existing.Images = rv.Images;
                            }
                        }
                    }
                    else
                    {
                        product.Variants.Add(new Ecom.Domain.Entities.Catalog.ProductVariant
                        {
                            Name = rv.Name,
                            SKU = rv.SKU,
                            Price = rv.Price,
                            StockQuantity = rv.StockQuantity,
                            IsActive = rv.IsActive,
                            OptionName = rv.OptionName,
                            OptionValue = rv.OptionValue,
                            Images = rv.Images ?? new()
                        });
                    }
                }
            }
            else 
            {
                product.Variants.Clear();
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
