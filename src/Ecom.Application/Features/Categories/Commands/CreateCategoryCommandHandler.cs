using MediatR;
using Microsoft.EntityFrameworkCore;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Domain.Entities.Catalog;

namespace Ecom.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Categories.AnyAsync(c => c.Slug == request.Slug, cancellationToken))
            {
                return Result.Failure(new[] { "A category with the specified slug already exists." });
            }

            var entity = new Category
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                IsFeatured = request.IsFeatured,
                DisplayOrder = request.DisplayOrder,
                MetaTitle = string.IsNullOrWhiteSpace(request.MetaTitle) ? request.Name : request.MetaTitle,
                MetaDescription = string.IsNullOrWhiteSpace(request.MetaDescription) ? request.Description : request.MetaDescription,
                Created = DateTime.UtcNow
            };

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
