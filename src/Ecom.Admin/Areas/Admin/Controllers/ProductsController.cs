using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ecom.Application.Features.Products.Queries;
using Ecom.Application.Features.Products.Commands;
using Ecom.Application.Features.Categories.Queries;
using Ecom.Application.Features.Brands.Queries;
using System.Threading.Tasks;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(command);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                await PopulateDropdowns();
                return View(command);
            }

            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction(nameof(Index));
        }
        
        private async Task PopulateDropdowns()
        {
            ViewBag.Categories = await _mediator.Send(new GetCategoriesQuery());
            ViewBag.Brands = await _mediator.Send(new GetBrandsQuery());
        }
    }
}
