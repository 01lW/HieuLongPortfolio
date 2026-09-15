using Microsoft.AspNetCore.Mvc;

namespace HieuLongPortfolio.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}