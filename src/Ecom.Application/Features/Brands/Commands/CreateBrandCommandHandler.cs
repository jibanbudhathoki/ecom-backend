using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Domain.Entities.Catalog;

namespace Ecom.Application.Features.Brands.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateBrandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Brands.AnyAsync(b => b.Slug == request.Slug, cancellationToken))
            {
                return Result.Failure(new[] { "A brand with the specified slug already exists." });
            }

            var entity = new Brand
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                MetaTitle = string.IsNullOrWhiteSpace(request.MetaTitle) ? request.Name : request.MetaTitle,
                MetaDescription = string.IsNullOrWhiteSpace(request.MetaDescription) ? request.Description : request.MetaDescription,
                Created = DateTime.UtcNow
            };

            _context.Brands.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
