using Microsoft.AspNetCore.Mvc;

namespace HieuLongPortfolio.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
