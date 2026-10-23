using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Domain.Entities.Catalog;

namespace Ecom.Application.Features.Collections.Commands
{
    public class UpdateCollectionCommandHandler : IRequestHandler<UpdateCollectionCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCollectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Collections
                .Include(c => c.ProductCollections)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return Result.Failure(new[] { "Collection not found." });
            }

            if (await _context.Collections.AnyAsync(c => c.Slug == request.Slug && c.Id != request.Id, cancellationToken))
            {
                return Result.Failure(new[] { "A collection with the specified slug already exists." });
            }

            entity.Name = request.Name;
            entity.Slug = request.Slug;
            entity.Description = request.Description;
            if (!string.IsNullOrEmpty(request.ImageUrl))
            {
                entity.ImageUrl = request.ImageUrl;
            }
            entity.Type = string.IsNullOrWhiteSpace(request.Type) ? "Manual" : request.Type;
            entity.ConditionsJson = request.ConditionsJson;
            entity.IsActive = request.IsActive;
            entity.MetaTitle = request.MetaTitle;
            entity.MetaDescription = request.MetaDescription;
            entity.LastModified = DateTime.UtcNow;

            // Sync assigned products
            entity.ProductCollections.Clear();
            if (request.ProductIds != null && request.ProductIds.Count > 0)
            {
                int order = 0;
                foreach (var productId in request.ProductIds.Distinct())
                {
                    entity.ProductCollections.Add(new ProductCollection
                    {
                        CollectionId = entity.Id,
                        ProductId = productId,
                        SortOrder = order++
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
