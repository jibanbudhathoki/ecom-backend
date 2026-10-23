using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Collections.Commands
{
    public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCollectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Collections.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                return Result.Failure(new[] { "Collection not found." });
            }

            _context.Collections.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
