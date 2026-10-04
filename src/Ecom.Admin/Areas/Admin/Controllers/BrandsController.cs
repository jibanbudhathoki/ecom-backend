using System.Threading.Tasks;
using Ecom.Application.Features.Brands.Commands;
using Ecom.Application.Features.Brands.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _mediator.Send(new GetBrandsQuery());
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandCommand command)
        {
            ViewData["Action"] = "Create";
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(command);
            }

            TempData["SuccessMessage"] = "Brand created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            // In the future, fetch from DB using id
            return View("Create");
        }
    }
}
