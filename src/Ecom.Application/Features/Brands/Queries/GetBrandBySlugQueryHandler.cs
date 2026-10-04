using System.Threading;
using System.Threading.Tasks;
using Ecom.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandBySlugQueryHandler : IRequestHandler<GetBrandBySlugQuery, BrandDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetBrandBySlugQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BrandDto?> Handle(GetBrandBySlugQuery request, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands
                .FirstOrDefaultAsync(b => b.Slug == request.Slug, cancellationToken);

            if (brand == null) return null;

            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Slug = brand.Slug,
                Description = brand.Description,
                ImageUrl = brand.ImageUrl,
                IsActive = brand.IsActive,
                MetaTitle = brand.MetaTitle,
                MetaDescription = brand.MetaDescription
            };
        }
    }
}
