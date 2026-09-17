using HieuLongPortfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace HieuLongPortfolio.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResumeRepository
            _resumeRepository;

        private readonly IWebHostEnvironment
            _environment;


        public ResumeController(
            IResumeRepository resumeRepository,
            IWebHostEnvironment environment)
        {
            _resumeRepository =
                resumeRepository;

            _environment =
                environment;
        }


        // =========================================
        // RESUME PAGE
        // =========================================

        public async Task<IActionResult> Index()
        {
            var resume =
                await _resumeRepository.GetAsync();


            return View(resume);
        }


        // =========================================
        // RESUME PDF
        // =========================================

        [HttpGet]
        public IActionResult Pdf(
            bool download = false)
        {
            var filePath =
                Path.Combine(
                    _environment.ContentRootPath,
                    "App_Data",
                    "HieuLongResume.pdf");


            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }


            if (download)
            {
                return PhysicalFile(
                    filePath,
                    "application/pdf",
                    "HieuLongResume.pdf",
                    enableRangeProcessing: true);
            }


            return PhysicalFile(
                filePath,
                "application/pdf",
                enableRangeProcessing: true);
        }
    }
}