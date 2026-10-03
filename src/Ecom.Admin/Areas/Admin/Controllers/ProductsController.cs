using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            return View("Create");
        }
    }
}
