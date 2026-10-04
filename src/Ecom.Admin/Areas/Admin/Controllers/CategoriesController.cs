using Ecom.Application.Features.Categories.Commands;
using Ecom.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index([FromQuery] Ecom.Application.Features.Categories.Queries.GetCategoriesQuery query)
        {
            var categories = await _mediator.Send(query ?? new Ecom.Application.Features.Categories.Queries.GetCategoriesQuery());
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var catRes = await _mediator.Send(new Ecom.Application.Features.Categories.Queries.GetCategoriesQuery { PageSize = 1000 });
                ViewBag.Categories = catRes.Items;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                var catRes = await _mediator.Send(new Ecom.Application.Features.Categories.Queries.GetCategoriesQuery { PageSize = 1000 });
                ViewBag.Categories = catRes.Items;
                return View(command);
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                var catRes = await _mediator.Send(new Ecom.Application.Features.Categories.Queries.GetCategoriesQuery { PageSize = 1000 });
                ViewBag.Categories = catRes.Items;
                return View(command);
            }

            TempData["SuccessMessage"] = "Category created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string slug)
        {
            var category = await _mediator.Send(new GetCategoryBySlugQuery(slug));
            if (category == null) return NotFound();

            var command = new UpdateCategoryCommand
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Description = category.Description,
                ParentCategoryId = category.ParentId,
                ImageUrl = category.ImageUrl,
                IsActive = category.IsActive,
                IsFeatured = category.IsFeatured,
                DisplayOrder = category.SortOrder,
                MetaTitle = category.MetaTitle,
                MetaDescription = category.MetaDescription
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid) return View(command);

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error);
                return View(command);
            }

            TempData["SuccessMessage"] = "Category updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (result.Succeeded) TempData["SuccessMessage"] = "Category deleted successfully!";
            else TempData["ErrorMessage"] = "Failed to delete category.";
            
            return RedirectToAction(nameof(Index));
        }
    }
}
