using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BannersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
