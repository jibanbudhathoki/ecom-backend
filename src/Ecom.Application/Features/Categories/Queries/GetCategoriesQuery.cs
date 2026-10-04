using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Categories.Queries
{
    public class GetCategoriesQuery : PaginationRequest, IRequest<PagedResult<CategoryDto>>
    {
    }
}
