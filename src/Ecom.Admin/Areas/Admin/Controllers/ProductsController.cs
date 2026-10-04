using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ecom.Application.Features.Products.Queries;
using Ecom.Application.Features.Products.Commands;
using Ecom.Application.Features.Categories.Queries;
using Ecom.Application.Features.Brands.Queries;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        public async Task<IActionResult> Index([FromQuery] GetProductsQuery query)
        {
            var products = await _mediator.Send(query ?? new GetProductsQuery());
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
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        System.Console.WriteLine($"ModelState Error: {error.ErrorMessage} / {error.Exception?.Message}");
                    }
                }
                await PopulateDropdowns();
                return View(command);
            }
            
            // Upload main images
            if (command.ImageFiles != null && command.ImageFiles.Count > 0)
            {
                foreach (var file in command.ImageFiles)
                {
                    var url = await SaveFileAsync(file);
                    if (!string.IsNullOrEmpty(url))
                    {
                        command.Images.Add(url);
                        // Also set the main ImageUrl for backward compatibility
                        if (string.IsNullOrEmpty(command.ImageUrl))
                        {
                            command.ImageUrl = url;
                        }
                    }
                }
            }
            
            // Upload variant images
            foreach (var variant in command.Variants)
            {
                if (variant.ImageFiles != null && variant.ImageFiles.Count > 0)
                {
                    foreach (var file in variant.ImageFiles)
                    {
                        var url = await SaveFileAsync(file);
                        if (!string.IsNullOrEmpty(url))
                        {
                            variant.Images.Add(url);
                        }
                    }
                }
            }

            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                System.IO.File.AppendAllText("med_errors.txt", "MED ERROR: " + string.Join(", ", result.Errors) + "\n");
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
        
        [HttpGet]
        public async Task<IActionResult> Edit(string slug)
        {
            var product = await _mediator.Send(new GetProductBySlugQuery(slug));
            if (product == null)
            {
                return NotFound();
            }

            var command = new UpdateProductCommand
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Price = product.Price,
                OldPrice = product.OldPrice,
                CostPrice = product.CostPrice,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                ManageStock = product.ManageStock,
                SKU = product.SKU,
                Barcode = product.Barcode,
                ImageUrl = product.ImageUrl,
                MetaTitle = product.MetaTitle,
                MetaDescription = product.MetaDescription,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Images = product.Images,
                Specifications = product.Specifications,
                Highlights = product.Highlights
            };
            
            foreach(var v in product.Variants)
            {
                command.Variants.Add(new UpdateProductVariantCommand
                {
                    Id = v.Id,
                    Name = v.Name,
                    SKU = v.SKU,
                    Price = v.Price,
                    StockQuantity = v.StockQuantity,
                    IsActive = v.IsActive,
                    Images = v.Images,
                    OptionName = v.OptionName,
                    OptionValue = v.OptionValue
                });
            }

            await PopulateDropdowns();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductCommand command)
        {
            Console.WriteLine("============= EDIT POST CALLED =============");
            if(!ModelState.IsValid) { Console.WriteLine("============= MODEL STATE INVALID ============="); }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                System.IO.File.AppendAllText("model_state_errors.txt", "EDIT ERROR: " + string.Join(", ", errors) + "\n");
                await PopulateDropdowns();
                return View(command);
            }
            
            // Upload main images
            if (command.ImageFiles != null && command.ImageFiles.Count > 0)
            {
                foreach (var file in command.ImageFiles)
                {
                    var url = await SaveFileAsync(file);
                    if (!string.IsNullOrEmpty(url))
                    {
                        command.Images.Add(url);
                        if (string.IsNullOrEmpty(command.ImageUrl))
                        {
                            command.ImageUrl = url;
                        }
                    }
                }
            }
            
            // Upload variant images
            if (command.Variants != null)
            {
                foreach (var variant in command.Variants)
                {
                    if (variant.ImageFiles != null && variant.ImageFiles.Count > 0)
                    {
                        foreach (var file in variant.ImageFiles)
                        {
                            var url = await SaveFileAsync(file);
                            if (!string.IsNullOrEmpty(url))
                            {
                                variant.Images.Add(url);
                            }
                        }
                    }
                }
            }

            var result = await _mediator.Send(command);
            if (!result.Succeeded)
            {
                System.IO.File.AppendAllText("med_errors.txt", "MED ERROR: " + string.Join(", ", result.Errors) + "\n");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                await PopulateDropdowns();
                return View(command);
            }

            TempData["SuccessMessage"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete product.";
            }
            return RedirectToAction(nameof(Index));
        }
        
        private async Task PopulateDropdowns()
        {
            var catRes = await _mediator.Send(new GetCategoriesQuery { PageSize = 1000 });
            ViewBag.Categories = catRes.Items;
            var brandRes = await _mediator.Send(new GetBrandsQuery { PageSize = 1000 });
            ViewBag.Brands = brandRes.Items;
        }
        
        private async Task<string> SaveFileAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/uploads/products/" + uniqueFileName;
        }
    }
}
