namespace Ecom.Application.Features.Products.Queries
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        
        public string? OptionName { get; set; }
        public string? OptionValue { get; set; }
        public List<string> Images { get; set; } = new();
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        
        public string? SKU { get; set; }
        public int StockQuantity { get; set; }
        public int VariantCount { get; set; }
        
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
    }
}
