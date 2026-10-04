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

        public async Task<IActionResult> Index([FromQuery] Ecom.Application.Features.Brands.Queries.GetBrandsQuery query)
        {
            var brands = await _mediator.Send(query ?? new Ecom.Application.Features.Brands.Queries.GetBrandsQuery());
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

        [HttpGet]
        public async Task<IActionResult> Edit(string slug)
        {
            var brand = await _mediator.Send(new GetBrandBySlugQuery(slug));
            if (brand == null) return NotFound();

            var command = new UpdateBrandCommand
            {
                Id = brand.Id,
                Name = brand.Name,
                Slug = brand.Slug,
                Description = brand.Description,
                ImageUrl = brand.ImageUrl,
                IsActive = brand.IsActive,
                MetaTitle = brand.MetaTitle,
                MetaDescription = brand.MetaDescription
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBrandCommand command)
        {
            if (!ModelState.IsValid) return View(command);

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error);
                return View(command);
            }

            TempData["SuccessMessage"] = "Brand updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (result.Succeeded) TempData["SuccessMessage"] = "Brand deleted successfully!";
            else TempData["ErrorMessage"] = "Failed to delete brand.";
            
            return RedirectToAction(nameof(Index));
        }
    }
}
