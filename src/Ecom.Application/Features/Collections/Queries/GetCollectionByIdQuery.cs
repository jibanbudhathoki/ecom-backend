using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;

namespace Ecom.Application.Features.Collections.Queries
{
    public class GetCollectionByIdQuery : IRequest<CollectionDto?>
    {
        public int Id { get; set; }

        public GetCollectionByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetCollectionByIdQueryHandler : IRequestHandler<GetCollectionByIdQuery, CollectionDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCollectionByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CollectionDto?> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Collections
                .AsNoTracking()
                .Include(c => c.ProductCollections)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity == null) return null;

            return new CollectionDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Type = entity.Type,
                ConditionsJson = entity.ConditionsJson,
                IsActive = entity.IsActive,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                ProductsCount = entity.ProductCollections.Count,
                ProductIds = entity.ProductCollections.OrderBy(pc => pc.SortOrder).Select(pc => pc.ProductId).ToList()
            };
        }
    }
}
