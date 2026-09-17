using HieuLongPortfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace HieuLongPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository
            _projectRepository;


        public ProjectsController(
            IProjectRepository projectRepository)
        {
            _projectRepository =
                projectRepository;
        }


        // =========================================
        // PROJECT LIST
        // =========================================

        public async Task<IActionResult> Index()
        {
            var projects =
                await _projectRepository
                    .GetAllAsync();


            return View(projects);
        }


        // =========================================
        // PROJECT DETAILS
        // =========================================

        public async Task<IActionResult>
            Details(int id)
        {
            var project =
                await _projectRepository
                    .GetByIdAsync(id);


            if (project == null)
            {
                return NotFound();
            }


            return View(project);
        }


        // =========================================
        // PC BUILDS
        // =========================================

        public IActionResult PCBuilds()
        {
            return View();
        }
    }
}