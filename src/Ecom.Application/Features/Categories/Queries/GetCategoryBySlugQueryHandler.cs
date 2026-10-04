using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;

namespace Ecom.Application.Features.Categories.Queries
{
    public class GetCategoryBySlugQueryHandler : IRequestHandler<GetCategoryBySlugQuery, CategoryDetailDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoryBySlugQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryDetailDto?> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Slug == request.Slug)
                .Select(c => new CategoryDetailDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug,
                    Description = c.Description,
                    ParentId = c.ParentCategoryId,
                    ParentName = c.ParentCategory != null ? c.ParentCategory.Name : null,
                    ImageUrl = c.ImageUrl,
                    IsActive = c.IsActive,
                    IsFeatured = c.IsFeatured,
                    SortOrder = c.DisplayOrder,
                    ProductCount = 0, // Hardcoded for now
                    CreatedAt = c.Created,
                    UpdatedAt = c.LastModified,
                    SubCategories = c.SubCategories.Select(sc => new CategoryDto
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Slug = sc.Slug,
                        Description = sc.Description,
                        ParentId = sc.ParentCategoryId,
                        ImageUrl = sc.ImageUrl,
                        IsActive = sc.IsActive,
                        IsFeatured = sc.IsFeatured,
                        SortOrder = sc.DisplayOrder
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return category;
        }
    }
}
