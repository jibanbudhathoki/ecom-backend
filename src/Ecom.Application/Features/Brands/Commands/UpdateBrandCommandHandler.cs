using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Brands.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBrandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Brands.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) return Result.Failure(new[] { "Brand not found." });

            entity.Name = request.Name;
            entity.Slug = request.Slug;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
