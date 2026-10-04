using Ecom.Application.Features.Brands.Queries;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Ecom.Application.Common.Models;
using MediatR;

namespace Ecom.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BrandDto>>> GetAll([FromQuery] GetBrandsQuery query)
        {
            var brands = await _mediator.Send(query ?? new GetBrandsQuery());
            return Ok(brands);
        }
    }
}
