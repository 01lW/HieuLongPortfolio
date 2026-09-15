using Microsoft.AspNetCore.Mvc;
using HieuLongPortfolio.Models;

namespace HieuLongPortfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private static readonly List<Project> Projects = new()
        {
            new Project
            {
                Id = 1,
                Title = "Pocket Forest",
                ShortDescription =
                    "A smart terrarium monitoring interface for managing multiple terrariums.",
                Description =
                    "Pocket Forest is a team-based smart terrarium project. " +
                    "The interface allows users to manage terrariums, monitor temperature " +
                    "and humidity, control lighting schedules, maintain journals and archive terrariums.",
                Technologies =
                    "HTML, CSS, JavaScript, ESP32, JSON",
                ImageUrl =
                    "/images/pocketforest-placeholder.jpg",
                GitHubUrl =
                    "",
                DemoUrl =
                    ""
            },

            new Project
            {
                Id = 2,
                Title = "Event Manager",
                ShortDescription =
                    "An ASP.NET Core event-management web application.",
                Description =
                    "A web application for creating and managing events. " +
                    "The project includes attendee registration, authentication, " +
                    "role-based authorization, real-time SignalR updates and cloud integration.",
                Technologies =
                    "C#, ASP.NET Core MVC, Entity Framework Core, SignalR, Azure SQL",
                ImageUrl =
                    "/images/eventmanager-placeholder.jpg",
                GitHubUrl =
                    "",
                DemoUrl =
                    ""
            },

            new Project
            {
                Id = 3,
                Title = "IoT Project",
                ShortDescription =
                    "An IoT system combining hardware sensors and software.",
                Description =
                    "An Internet of Things project involving sensor data, hardware integration " +
                    "and a software interface for monitoring connected devices.",
                Technologies =
                    "Node-RED, JavaScript, IoT, Embedded Systems",
                ImageUrl =
                    "/images/iot-placeholder.jpg",
                GitHubUrl =
                    "",
                DemoUrl =
                    ""
            }
        };

        public IActionResult Index()
        {
            return View(Projects);
        }

        public IActionResult Details(int id)
        {
            Project? project = Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}