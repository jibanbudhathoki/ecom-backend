using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteBrandCommand(int id) => Id = id;
    }
}
