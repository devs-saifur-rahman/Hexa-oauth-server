using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexa.Web.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
