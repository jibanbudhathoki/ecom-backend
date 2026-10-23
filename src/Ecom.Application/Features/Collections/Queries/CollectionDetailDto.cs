using System.Collections.Generic;
using Ecom.Application.Features.Products.Queries;

namespace Ecom.Application.Features.Collections.Queries
{
    public class CollectionDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string Type { get; set; } = "Manual";
        public string? ConditionsJson { get; set; }
        public bool IsActive { get; set; }
        public int ProductsCount { get; set; }

        public List<ProductDto> Products { get; set; } = new();
    }
}
