using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) return Result.Failure(new[] { "Category not found." });

            entity.Name = request.Name;
            entity.Slug = request.Slug;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
