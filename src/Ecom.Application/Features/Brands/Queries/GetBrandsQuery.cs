using System.Collections.Generic;
using MediatR;

namespace Ecom.Application.Features.Brands.Queries
{
    public class GetBrandsQuery : IRequest<List<BrandDto>>
    {
    }
}
