using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Brands.Commands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBrandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Brands.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) return Result.Failure(new[] { "Brand not found." });

            _context.Brands.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
