using HieuLongPortfolio.Models;
using HieuLongPortfolio.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HieuLongPortfolio.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IConfiguration
            _configuration;

        private readonly IProjectRepository
            _projectRepository;

        private readonly IResumeRepository
            _resumeRepository;

        private readonly IWebHostEnvironment
            _environment;


        public AdminController(
            IConfiguration configuration,
            IProjectRepository projectRepository,
            IResumeRepository resumeRepository,
            IWebHostEnvironment environment)
        {
            _configuration =
                configuration;

            _projectRepository =
                projectRepository;

            _resumeRepository =
                resumeRepository;

            _environment =
                environment;
        }


        // =========================================
        // DASHBOARD
        // =========================================

        public async Task<IActionResult> Index()
        {
            var projects =
                await _projectRepository
                    .GetAllAsync();


            ViewBag.ProjectCount =
                projects.Count;


            return View();
        }


        // =========================================
        // LOGIN - GET
        // =========================================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(
            string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated
                == true)
            {
                return RedirectToAction(
                    nameof(Index));
            }


            var model =
                new AdminLoginViewModel
                {
                    ReturnUrl = returnUrl
                };


            return View(model);
        }


        // =========================================
        // LOGIN - POST
        // =========================================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            const string adminUsername =
                "admin";


            var adminPassword =
                _configuration[
                    "AdminCredentials:Password"];


            if (string.IsNullOrWhiteSpace(
                    adminPassword))
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Admin login is not configured.");

                return View(model);
            }


            if (model.Username != adminUsername ||
                model.Password != adminPassword)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid username or password.");

                return View(model);
            }


            var claims =
                new List<Claim>
                {
                    new Claim(
                        ClaimTypes.Name,
                        adminUsername),

                    new Claim(
                        ClaimTypes.Role,
                        "Admin")
                };


            var identity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults
                        .AuthenticationScheme);


            var principal =
                new ClaimsPrincipal(identity);


            var properties =
                new AuthenticationProperties
                {
                    IsPersistent = false,

                    ExpiresUtc =
                        DateTimeOffset.UtcNow
                            .AddHours(8)
                };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults
                    .AuthenticationScheme,
                principal,
                properties);


            if (!string.IsNullOrWhiteSpace(
                    model.ReturnUrl)
                &&
                Url.IsLocalUrl(
                    model.ReturnUrl))
            {
                return LocalRedirect(
                    model.ReturnUrl);
            }


            return RedirectToAction(
                nameof(Index));
        }


        // =========================================
        // LOGOUT
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults
                    .AuthenticationScheme);


            return RedirectToAction(
                nameof(Login));
        }


        // =========================================
        // MANAGE PROJECTS
        // =========================================

        [HttpGet]
        public async Task<IActionResult>
            ManageProjects()
        {
            var projects =
                await _projectRepository
                    .GetAllAsync();


            return View(projects);
        }


        // =========================================
        // CREATE PROJECT - GET
        // =========================================

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View(
                new AdminProjectViewModel());
        }


        // =========================================
        // CREATE PROJECT - POST
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            CreateProject(
                AdminProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var project =
                ToProject(model);


            await _projectRepository
                .CreateAsync(project);


            TempData["SuccessMessage"] =
                "Project added successfully.";


            return RedirectToAction(
                nameof(ManageProjects));
        }


        // =========================================
        // EDIT PROJECT - GET
        // =========================================

        [HttpGet]
        public async Task<IActionResult>
            EditProject(int id)
        {
            var project =
                await _projectRepository
                    .GetByIdAsync(id);


            if (project == null)
            {
                return NotFound();
            }


            return View(
                ToViewModel(project));
        }


        // =========================================
        // EDIT PROJECT - POST
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            EditProject(
                int id,
                AdminProjectViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var project =
                ToProject(model);


            var updated =
                await _projectRepository
                    .UpdateAsync(project);


            if (!updated)
            {
                return NotFound();
            }


            TempData["SuccessMessage"] =
                "Project updated successfully.";


            return RedirectToAction(
                nameof(ManageProjects));
        }


        // =========================================
        // DELETE PROJECT
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteProject(int id)
        {
            var deleted =
                await _projectRepository
                    .DeleteAsync(id);


            if (deleted)
            {
                TempData["SuccessMessage"] =
                    "Project deleted successfully.";
            }


            return RedirectToAction(
                nameof(ManageProjects));
        }


        // =========================================
        // MANAGE RESUME
        // =========================================

        [HttpGet]
        public async Task<IActionResult>
            ManageResume()
        {
            var resume =
                await _resumeRepository
                    .GetAsync();


            var model =
                ToAdminResumeViewModel(
                    resume);


            model.CurrentResumeExists =
                System.IO.File.Exists(
                    GetResumePath());


            return View(model);
        }


        // =========================================
        // SAVE WEB RESUME
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            SaveWebResume(
                AdminResumeViewModel model)
        {
            var resume =
                ToResumeData(model);


            await _resumeRepository
                .SaveAsync(resume);


            TempData["SuccessMessage"] =
                "Web resume updated successfully.";


            return RedirectToAction(
                nameof(ManageResume));
        }


        // =========================================
        // UPLOAD PDF RESUME
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult>
            UploadResume(
                AdminResumeViewModel model)
        {
            if (model.ResumeFile == null ||
                model.ResumeFile.Length == 0)
            {
                TempData["ErrorMessage"] =
                    "Please select a PDF file.";

                return RedirectToAction(
                    nameof(ManageResume));
            }


            var extension =
                Path.GetExtension(
                    model.ResumeFile.FileName);


            if (!string.Equals(
                    extension,
                    ".pdf",
                    StringComparison
                        .OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] =
                    "Only PDF files are allowed.";

                return RedirectToAction(
                    nameof(ManageResume));
            }


            if (model.ResumeFile.Length >
                10_000_000)
            {
                TempData["ErrorMessage"] =
                    "The PDF must be smaller than 10 MB.";

                return RedirectToAction(
                    nameof(ManageResume));
            }


            var resumePath =
                GetResumePath();


            Directory.CreateDirectory(
                Path.GetDirectoryName(
                    resumePath)!);


            await using var stream =
                new FileStream(
                    resumePath,
                    FileMode.Create);


            await model.ResumeFile
                .CopyToAsync(stream);


            TempData["SuccessMessage"] =
                "Resume PDF updated successfully.";


            return RedirectToAction(
                nameof(ManageResume));
        }


        // =========================================
        // RESUME PDF PATH
        // =========================================

        private string GetResumePath()
        {
            return Path.Combine(
                _environment.ContentRootPath,
                "App_Data",
                "HieuLongResume.pdf");
        }


        // =========================================
        // PROJECT -> ADMIN VIEW MODEL
        // =========================================

        private static AdminProjectViewModel
            ToViewModel(Project project)
        {
            return new AdminProjectViewModel
            {
                Id =
                    project.Id,

                Title =
                    project.Title,

                ShortDescription =
                    project.ShortDescription,

                Description =
                    project.Description,

                Technologies =
                    project.Technologies,

                Hardware =
                    project.Hardware,

                ProjectType =
                    project.ProjectType,

                Role =
                    project.Role,

                Team =
                    project.Team,

                MyContribution =
                    project.MyContribution,

                SystemArchitecture =
                    project.SystemArchitecture,

                ImageUrl =
                    project.ImageUrl,

                GitHubUrl =
                    project.GitHubUrl,

                DemoUrl =
                    project.DemoUrl,

                GalleryImagesText =
                    string.Join(
                        Environment.NewLine,
                        project.GalleryImages),

                FeaturesText =
                    string.Join(
                        Environment.NewLine,
                        project.Features),

                TestingText =
                    string.Join(
                        Environment.NewLine,
                        project.Testing),

                ChallengesText =
                    string.Join(
                        Environment.NewLine,
                        project.Challenges)
            };
        }


        // =========================================
        // ADMIN VIEW MODEL -> PROJECT
        // =========================================

        private static Project
            ToProject(
                AdminProjectViewModel model)
        {
            return new Project
            {
                Id =
                    model.Id,

                Title =
                    model.Title?.Trim()
                    ?? string.Empty,

                ShortDescription =
                    model.ShortDescription?.Trim()
                    ?? string.Empty,

                Description =
                    model.Description?.Trim()
                    ?? string.Empty,

                Technologies =
                    model.Technologies?.Trim()
                    ?? string.Empty,

                Hardware =
                    model.Hardware?.Trim()
                    ?? string.Empty,

                ProjectType =
                    model.ProjectType?.Trim()
                    ?? string.Empty,

                Role =
                    model.Role?.Trim()
                    ?? string.Empty,

                Team =
                    model.Team?.Trim()
                    ?? string.Empty,

                MyContribution =
                    model.MyContribution?.Trim()
                    ?? string.Empty,

                SystemArchitecture =
                    model.SystemArchitecture?.Trim()
                    ?? string.Empty,

                ImageUrl =
                    model.ImageUrl?.Trim()
                    ?? string.Empty,

                GitHubUrl =
                    model.GitHubUrl?.Trim()
                    ?? string.Empty,

                DemoUrl =
                    model.DemoUrl?.Trim()
                    ?? string.Empty,

                GalleryImages =
                    SplitLines(
                        model.GalleryImagesText),

                Features =
                    SplitLines(
                        model.FeaturesText),

                Testing =
                    SplitLines(
                        model.TestingText),

                Challenges =
                    SplitLines(
                        model.ChallengesText)
            };
        }


        // =========================================
        // RESUME -> ADMIN VIEW MODEL
        // =========================================

        private static AdminResumeViewModel
            ToAdminResumeViewModel(
                ResumeData resume)
        {
            return new AdminResumeViewModel
            {
                ProfessionalSummary =
                    resume.ProfessionalSummary,


                EducationProgram =
                    resume.Education.Program,

                EducationSchool =
                    resume.Education.School,

                EducationLocation =
                    resume.Education.Location,

                EducationDate =
                    resume.Education.Date,


                Experiences =
                    resume.Experiences
                        .Select(experience =>
                            new AdminResumeExperienceViewModel
                            {
                                Title =
                                    experience.Title,

                                Company =
                                    experience.Company,

                                Location =
                                    experience.Location,

                                Date =
                                    experience.Date,

                                BulletsText =
                                    string.Join(
                                        Environment.NewLine,
                                        experience.Bullets)
                            })
                        .ToList(),


                Projects =
                    resume.Projects
                        .Select(project =>
                            new AdminResumeProjectViewModel
                            {
                                Title =
                                    project.Title,

                                Technologies =
                                    project.Technologies,

                                BulletsText =
                                    string.Join(
                                        Environment.NewLine,
                                        project.Bullets)
                            })
                        .ToList(),


                PcHardwareSupport =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.PcHardwareSupport),

                Languages =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.Languages),

                FrameworksTechnologies =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.FrameworksTechnologies),

                Databases =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.Databases),

                CloudDeployment =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.CloudDeployment),

                EmbeddedRealTime =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.EmbeddedRealTime),

                Tools =
                    string.Join(
                        Environment.NewLine,
                        resume.Skills.Tools)
            };
        }


        // =========================================
        // ADMIN VIEW MODEL -> RESUME
        // =========================================

        private static ResumeData
            ToResumeData(
                AdminResumeViewModel model)
        {
            return new ResumeData
            {
                ProfessionalSummary =
                    model.ProfessionalSummary?.Trim()
                    ?? string.Empty,


                Education =
                    new ResumeEducation
                    {
                        Program =
                            model.EducationProgram?.Trim()
                            ?? string.Empty,

                        School =
                            model.EducationSchool?.Trim()
                            ?? string.Empty,

                        Location =
                            model.EducationLocation?.Trim()
                            ?? string.Empty,

                        Date =
                            model.EducationDate?.Trim()
                            ?? string.Empty
                    },


                Experiences =
                    (model.Experiences
                        ?? new List<AdminResumeExperienceViewModel>())
                    .Where(experience =>
                        !string.IsNullOrWhiteSpace(
                            experience.Title))
                    .Select(experience =>
                        new ResumeExperience
                        {
                            Title =
                                experience.Title.Trim(),

                            Company =
                                experience.Company?.Trim()
                                ?? string.Empty,

                            Location =
                                experience.Location?.Trim()
                                ?? string.Empty,

                            Date =
                                experience.Date?.Trim()
                                ?? string.Empty,

                            Bullets =
                                SplitLines(
                                    experience.BulletsText)
                        })
                    .ToList(),


                Projects =
                    (model.Projects
                        ?? new List<AdminResumeProjectViewModel>())
                    .Where(project =>
                        !string.IsNullOrWhiteSpace(
                            project.Title))
                    .Select(project =>
                        new ResumeProject
                        {
                            Title =
                                project.Title.Trim(),

                            Technologies =
                                project.Technologies?.Trim()
                                ?? string.Empty,

                            Bullets =
                                SplitLines(
                                    project.BulletsText)
                        })
                    .ToList(),


                Skills =
                    new ResumeSkills
                    {
                        PcHardwareSupport =
                            SplitLines(
                                model.PcHardwareSupport),

                        Languages =
                            SplitLines(
                                model.Languages),

                        FrameworksTechnologies =
                            SplitLines(
                                model.FrameworksTechnologies),

                        Databases =
                            SplitLines(
                                model.Databases),

                        CloudDeployment =
                            SplitLines(
                                model.CloudDeployment),

                        EmbeddedRealTime =
                            SplitLines(
                                model.EmbeddedRealTime),

                        Tools =
                            SplitLines(
                                model.Tools)
                    }
            };
        }


        // =========================================
        // TEXTAREA -> LIST
        // =========================================

        private static List<string>
            SplitLines(string? value)
        {
            if (string.IsNullOrWhiteSpace(
                    value))
            {
                return new List<string>();
            }


            return value
                .Split(
                    new[]
                    {
                        "\r\n",
                        "\n"
                    },
                    StringSplitOptions
                        .RemoveEmptyEntries)
                .Select(line =>
                    line.Trim())
                .Where(line =>
                    !string.IsNullOrWhiteSpace(
                        line))
                .ToList();
        }
    }
}