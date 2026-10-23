using MediatR;

namespace Ecom.Application.Features.Collections.Queries
{
    public class GetCollectionBySlugQuery : IRequest<CollectionDetailDto?>
    {
        public string Slug { get; set; }

        public GetCollectionBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
