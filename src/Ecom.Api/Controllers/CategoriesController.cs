using Ecom.Application.Features.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Ecom.Application.Common.Models;
using MediatR;

namespace Ecom.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetAll([FromQuery] GetCategoriesQuery query)
        {
            var categories = await _mediator.Send(query ?? new GetCategoriesQuery());
            return Ok(categories);
        }
    }
}
