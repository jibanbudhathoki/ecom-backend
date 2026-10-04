using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id) => Id = id;
    }
}
