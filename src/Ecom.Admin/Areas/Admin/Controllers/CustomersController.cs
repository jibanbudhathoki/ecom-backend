using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
