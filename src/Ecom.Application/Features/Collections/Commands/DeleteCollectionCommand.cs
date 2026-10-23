using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Collections.Commands
{
    public class DeleteCollectionCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteCollectionCommand(int id)
        {
            Id = id;
        }
    }
}
