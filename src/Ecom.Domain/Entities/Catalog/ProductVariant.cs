using Ecom.Domain.Common;

namespace Ecom.Domain.Entities.Catalog
{
    public class ProductVariant : BaseAuditableEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string Name { get; set; } = string.Empty; // e.g. "Large / Red"
        public string? SKU { get; set; }
        
        public decimal? Price { get; set; } // If null, fallback to Product.Price
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        
        public List<string> Images { get; set; } = new List<string>();
        public string? OptionName { get; set; }
        public string? OptionValue { get; set; }
    }
}
