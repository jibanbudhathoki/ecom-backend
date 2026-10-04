using Ecom.Application.Features.Products.Queries;
using Ecom.Application.Features.Products.Commands;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Ecom.Application.Common.Models;
using MediatR;
using System.Threading.Tasks;

namespace Ecom.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ListResponse<ProductDto>>> GetAll()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(new ListResponse<ProductDto>(products));
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductDetailDto>> GetBySlug(string slug)
        {
            var product = await _mediator.Send(new GetProductBySlugQuery(slug));
            
            if (product == null)
            {
                return NotFound(new { Message = $"Product with slug '{slug}' not found." });
            }
            
            return Ok(product);
        }

    }
}
