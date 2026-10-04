
using MediatR;

namespace Ecom.Application.Features.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
