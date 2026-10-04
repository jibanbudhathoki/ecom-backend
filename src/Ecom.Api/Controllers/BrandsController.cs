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
        public async Task<ActionResult<ListResponse<BrandDto>>> GetAll()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return Ok(new ListResponse<BrandDto>(brands));
        }
    }
}
