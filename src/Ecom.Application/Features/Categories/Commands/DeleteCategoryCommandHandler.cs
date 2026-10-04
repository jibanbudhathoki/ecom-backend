using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Ecom.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null) return Result.Failure(new[] { "Category not found." });

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
