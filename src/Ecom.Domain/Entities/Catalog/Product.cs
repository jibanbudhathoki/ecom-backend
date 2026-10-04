using Ecom.Domain.Common;

namespace Ecom.Domain.Entities.Catalog
{
    public class Product : BaseAuditableEntity
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
        public Category Category { get; set; } = null!;
        
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        
        public List<string> Images { get; set; } = new List<string>();
        public List<ProductSpecification> Specifications { get; set; } = new List<ProductSpecification>();
        public List<string> Highlights { get; set; } = new List<string>();
    }
}
