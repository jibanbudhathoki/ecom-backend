using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandsQuery : PaginationRequest, IRequest<PagedResult<BrandDto>>
    {
    }
}
