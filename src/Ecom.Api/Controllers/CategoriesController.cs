using System.Collections.Generic;
using System.Threading.Tasks;
using Ecom.Application.Features.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Ecom.Application.Common.Models;
using Ecom.Application.Features.Categories.Queries;

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
        public async Task<ActionResult<ListResponse<CategoryDto>>> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(new ListResponse<CategoryDto>(categories));
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<CategoryDetailDto>> GetBySlug(string slug)
        {
            var category = await _mediator.Send(new GetCategoryBySlugQuery(slug));
            
            if (category == null)
            {
                return NotFound(new { Message = $"Category with slug '{slug}' not found." });
            }
            
            return Ok(category);
        }

    }
}
