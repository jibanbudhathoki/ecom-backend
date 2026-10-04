using System.Collections.Generic;
using Ecom.Domain.Common;

namespace Ecom.Domain.Entities.Catalog
{
    public class Brand : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        
        // SEO
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        
    }
}
