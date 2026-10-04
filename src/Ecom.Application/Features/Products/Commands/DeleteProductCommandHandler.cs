using MediatR;
using Ecom.Application.Common.Models;
using Ecom.Application.Common.Interfaces;

namespace Ecom.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (product == null)
            {
                return Result.Failure(new[] { "Product not found." });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
