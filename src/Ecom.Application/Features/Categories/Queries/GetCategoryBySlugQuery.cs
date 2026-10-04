using MediatR;

namespace Ecom.Application.Features.Categories.Queries
{
    public class GetCategoryBySlugQuery : IRequest<CategoryDetailDto?>
    {
        public string Slug { get; set; } = string.Empty;

        public GetCategoryBySlugQuery(string slug)
        {
            Slug = slug;
        }
    }
}
