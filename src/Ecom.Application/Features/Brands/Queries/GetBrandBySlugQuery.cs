using MediatR;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandBySlugQuery : IRequest<BrandDto?>
    {
        public string Slug { get; set; }

        public GetBrandBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
