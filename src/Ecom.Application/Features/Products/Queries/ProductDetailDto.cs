namespace Ecom.Application.Features.Products.Queries
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? CostPrice { get; set; }
        
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public bool ManageStock { get; set; }
        
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string? ImageUrl { get; set; }
        
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        
        public int? BrandId { get; set; }
        public string? BrandName { get; set; }
        
        public ICollection<ProductVariantDto> Variants { get; set; } = new List<ProductVariantDto>();
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
