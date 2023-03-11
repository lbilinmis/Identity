using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.CoreWebUI.Controllers
{
    public class FemaleController : Controller
    {
        [Authorize(Policy = "FemalePolicy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
