using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.CoreWebUI.Controllers
{
    [Authorize("Admin,Developer")]

    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
