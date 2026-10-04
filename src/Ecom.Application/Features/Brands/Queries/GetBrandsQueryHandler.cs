using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetBrandsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _context.Brands
                .AsNoTracking()
                .Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Slug = b.Slug,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    IsActive = b.IsActive
                })
                .ToListAsync(cancellationToken);

            return brands;
        }
    }
}
