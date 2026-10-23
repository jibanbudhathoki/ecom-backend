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
    public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateCollectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Collections.AnyAsync(c => c.Slug == request.Slug, cancellationToken))
            {
                return Result.Failure(new[] { "A collection with the specified slug already exists." });
            }

            var collection = new Collection
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Type = string.IsNullOrWhiteSpace(request.Type) ? "Manual" : request.Type,
                ConditionsJson = request.ConditionsJson,
                IsActive = request.IsActive,
                MetaTitle = string.IsNullOrWhiteSpace(request.MetaTitle) ? request.Name : request.MetaTitle,
                MetaDescription = string.IsNullOrWhiteSpace(request.MetaDescription) ? request.Description : request.MetaDescription,
                Created = DateTime.UtcNow
            };

            if (request.ProductIds != null && request.ProductIds.Count > 0)
            {
                int order = 0;
                foreach (var productId in request.ProductIds.Distinct())
                {
                    collection.ProductCollections.Add(new ProductCollection
                    {
                        ProductId = productId,
                        SortOrder = order++
                    });
                }
            }

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
