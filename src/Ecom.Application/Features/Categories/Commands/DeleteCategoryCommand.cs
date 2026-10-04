using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id) => Id = id;
    }
}
