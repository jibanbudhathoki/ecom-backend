using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ecom.Application.Features.Collections.Queries
{
    public class CollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string Type { get; set; } = "Manual";
        public string? ConditionsJson { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string? MetaTitle { get; set; }

        [JsonIgnore]
        public string? MetaDescription { get; set; }

        public int ProductsCount { get; set; }

        [JsonIgnore]
        public List<int> ProductIds { get; set; } = new();
    }
}
