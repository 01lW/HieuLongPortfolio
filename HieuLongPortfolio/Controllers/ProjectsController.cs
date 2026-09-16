using HieuLongPortfolio.Models;
using Microsoft.AspNetCore.Mvc;

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
                    "A smart terrarium monitoring and control system for managing multiple terrariums.",

                Description =
                    "Pocket Forest is a smart terrarium control system developed as a four-person " +
                    "team project. The system combines physical terrarium hardware with a companion " +
                    "web application that allows users to monitor environmental conditions, configure " +
                    "lighting schedules and manage multiple terrariums.",

                Technologies =
                    "HTML, CSS, JavaScript, ESP32, JSON",

                Hardware =
                    "ESP32, Arduino Nano, DHT11 Temperature & Humidity Sensor, RGB LEDs",

                ProjectType =
                    "School Team Project",

                Role =
                    "Front-End / UI Development",

                Team =
                    "4 Members",

                MyContribution =
                    "I focused primarily on the web interface for Pocket Forest. " +
                    "My work included the terrarium dashboard, temperature and humidity views, " +
                    "lighting controls, journal functionality, information pages, settings, " +
                    "navigation and terrarium management features.",

                SystemArchitecture =
                    "Pocket Forest uses an ESP32 as the main controller for the user interface, " +
                    "configuration and web application access. An Arduino Nano acts as a " +
                    "sub-controller responsible for direct control of the terrarium lighting " +
                    "and environmental sensors. The system was designed to support up to five " +
                    "terrarium lid modules.",

                ImageUrl =
                    "/images/pocket-forest/showcase.jpg",

                GalleryImages = new List<string>
                {
                    "/images/pocket-forest/interface-preview.png"
                },

                Features = new List<string>
                {
                    "Manage up to five terrariums",
                    "Temperature and humidity monitoring",
                    "Custom time-based lighting schedules",
                    "Sunrise and sunset lighting simulation",
                    "Journal system",
                    "Photo Mode",
                    "Information pages",
                    "QR and network access"
                },

                Testing = new List<string>
                {
                    "QR connectivity",
                    "Time-based lighting",
                    "Humidity response",
                    "Warning thresholds",
                    "Warning clearing",
                    "Ease of connection",
                    "Connection success rate"
                },

                Challenges = new List<string>
                {
                    "SPI bus conflict on the ESP32 board",
                    "Limited pin availability requiring an Arduino Nano sub-controller",
                    "DHT11 sensor accuracy limitations"
                },

                GitHubUrl = "",

                DemoUrl = ""
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
                    "role-based authorization, real-time updates and cloud integration.",

                Technologies =
                    "C#, ASP.NET Core MVC, Entity Framework Core, Identity, SignalR, Azure SQL",

                Hardware = "",

                ProjectType =
                    "Web Application",

                Role =
                    "Full-Stack Development",

                Team = "",

                MyContribution =
                    "I developed the event-management application using ASP.NET Core MVC. " +
                    "My work included event and attendee management, database integration, " +
                    "authentication, role-based authorization and real-time SignalR updates.",

                SystemArchitecture =
                    "The application follows the ASP.NET Core MVC architecture. " +
                    "Entity Framework Core handles database access, ASP.NET Core Identity " +
                    "provides authentication and role-based authorization, SignalR provides " +
                    "real-time communication, and Azure SQL provides cloud-based data storage.",

                ImageUrl = "",

                GalleryImages = new List<string>(),

                Features = new List<string>
                {
                    "Create and manage events",
                    "Attendee registration",
                    "Organizer and attendee roles",
                    "Authentication and authorization",
                    "Real-time attendee updates with SignalR",
                    "Cloud database integration"
                },

                Testing = new List<string>(),

                Challenges = new List<string>(),

                GitHubUrl = "",

                DemoUrl = ""
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

                Hardware = "",

                ProjectType =
                    "IoT Project",

                Role = "",

                Team = "",

                MyContribution = "",

                SystemArchitecture = "",

                ImageUrl = "",

                GalleryImages = new List<string>(),

                Features = new List<string>(),

                Testing = new List<string>(),

                Challenges = new List<string>(),

                GitHubUrl = "",

                DemoUrl = ""
            },

            new Project
            {
                Id = 4,

                Title = "Custom PC Builds",

                ShortDescription =
                    "Custom gaming PCs built, configured, tested and troubleshot for customers.",

                Description =
                    "A collection of custom gaming PCs I independently assembled, configured, " +
                    "tested, troubleshot and prepared for customers.",

                Technologies =
                    "PC Hardware, Windows, BIOS, Drivers, Diagnostics, Benchmarking",

                Hardware = "",

                ProjectType =
                    "Independent Technical Work",

                Role =
                    "PC Builder & Technician",

                Team =
                    "Independent",

                MyContribution = "",

                SystemArchitecture = "",

                ImageUrl = "/images/pc/cover.png",

                GalleryImages = new List<string>(),

                Features = new List<string>(),

                Testing = new List<string>(),

                Challenges = new List<string>(),

                GitHubUrl = "",

                DemoUrl = ""
            }
        };


        public IActionResult Index()
        {
            return View(Projects);
        }


        public IActionResult Details(int id)
        {
            Project? project =
                Projects.FirstOrDefault(project => project.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


        public IActionResult PCBuilds()
        {
            return View();
        }
    }
}