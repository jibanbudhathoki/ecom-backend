using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NavigationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
