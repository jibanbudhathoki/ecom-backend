using System.Collections.Generic;
using Ecom.Domain.Common;

namespace Ecom.Domain.Entities.Catalog
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public int DisplayOrder { get; set; }
        
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
