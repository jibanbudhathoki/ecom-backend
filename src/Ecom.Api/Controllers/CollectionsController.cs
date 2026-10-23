using Ecom.Application.Features.Collections.Queries;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Ecom.Application.Common.Models;
using MediatR;

namespace Ecom.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CollectionDto>>> GetAll([FromQuery] GetCollectionsQuery query)
        {
            var collections = await _mediator.Send(query ?? new GetCollectionsQuery());
            return Ok(collections);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CollectionDetailDto>> GetBySlug(string slug)
        {
            var collection = await _mediator.Send(new GetCollectionBySlugQuery(slug));
            if (collection == null) return NotFound();
            return Ok(collection);
        }
    }
}
