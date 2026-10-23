using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Collections.Queries
{
    public class GetCollectionsQuery : PaginationRequest, IRequest<PagedResult<CollectionDto>>
    {
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
    }
}
