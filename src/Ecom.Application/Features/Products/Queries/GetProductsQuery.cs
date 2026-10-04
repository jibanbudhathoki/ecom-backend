
using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
