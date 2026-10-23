using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ecom.Application.Features.Brands.Queries
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string? MetaTitle { get; set; }

        [JsonIgnore]
        public string? MetaDescription { get; set; }
    }
}
