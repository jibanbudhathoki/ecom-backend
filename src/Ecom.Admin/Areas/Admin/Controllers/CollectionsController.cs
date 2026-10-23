using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MediatR;
using Ecom.Application.Features.Collections.Commands;
using Ecom.Application.Features.Collections.Queries;
using Ecom.Application.Features.Products.Queries;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public CollectionsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        public async Task<IActionResult> Index([FromQuery] GetCollectionsQuery query)
        {
            var collections = await _mediator.Send(query ?? new GetCollectionsQuery());
            return View(collections);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            return View(new CreateCollectionCommand());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionCommand command, IFormFile? imageFile)
        {
            ViewData["Action"] = "Create";

            if (imageFile != null && imageFile.Length > 0)
            {
                command.ImageUrl = await SaveFileAsync(imageFile);
            }

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

            TempData["SuccessMessage"] = "Collection created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string slug)
        {
            ViewData["Action"] = "Edit";
            var collection = await _mediator.Send(new GetCollectionBySlugQuery(slug));
            if (collection == null) return NotFound();

            var command = new UpdateCollectionCommand
            {
                Id = collection.Id,
                Name = collection.Name,
                Slug = collection.Slug,
                Description = collection.Description,
                ImageUrl = collection.ImageUrl,
                Type = collection.Type,
                ConditionsJson = collection.ConditionsJson,
                IsActive = collection.IsActive,
                ProductIds = collection.Products.Select(p => p.Id).ToList()
            };

            ViewBag.AssignedProducts = collection.Products;

            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCollectionCommand command, IFormFile? imageFile)
        {
            ViewData["Action"] = "Edit";

            if (imageFile != null && imageFile.Length > 0)
            {
                command.ImageUrl = await SaveFileAsync(imageFile);
            }

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

            TempData["SuccessMessage"] = "Collection updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCollectionCommand(id));
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Collection deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete collection.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts(string? query)
        {
            var result = await _mediator.Send(new GetProductsQuery { SearchTerm = query ?? string.Empty, PageSize = 10 });
            return Json(result.Items.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                sku = p.SKU,
                price = p.Price,
                imageUrl = p.ImageUrl
            }));
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return string.Empty;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "collections");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/collections/" + uniqueFileName;
        }
    }
}
