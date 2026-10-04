using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductsQuery : PaginationRequest, IRequest<PagedResult<ProductDto>>
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
