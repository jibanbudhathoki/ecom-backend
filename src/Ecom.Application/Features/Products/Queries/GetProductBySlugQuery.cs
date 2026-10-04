using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductBySlugQuery : IRequest<ProductDetailDto?>
    {
        public string Slug { get; set; }

        public GetProductBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
