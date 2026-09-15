using Microsoft.AspNetCore.Mvc;

namespace HieuLongPortfolio.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
