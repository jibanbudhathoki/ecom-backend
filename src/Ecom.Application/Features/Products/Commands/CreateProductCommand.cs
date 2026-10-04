using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Products.Commands
{
    public class CreateProductVariantCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        
        public List<string> Images { get; set; } = new();
        public List<Microsoft.AspNetCore.Http.IFormFile>? ImageFiles { get; set; }
        public string? OptionName { get; set; }
        public string? OptionValue { get; set; }
    }

    public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? CostPrice { get; set; }
        
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool ManageStock { get; set; } = true;
        
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string? ImageUrl { get; set; }
        
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        
        public List<string> Images { get; set; } = new();
        public List<Microsoft.AspNetCore.Http.IFormFile>? ImageFiles { get; set; }
        public List<Ecom.Domain.Entities.Catalog.ProductSpecification> Specifications { get; set; } = new();
        public List<string> Highlights { get; set; } = new();
        
        public List<CreateProductVariantCommand> Variants { get; set; } = new List<CreateProductVariantCommand>();
    }
}
