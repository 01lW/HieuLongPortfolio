using HieuLongPortfolio.Models;

namespace HieuLongPortfolio.Services
{
    public static class ProjectSeedData
    {
        public static List<Project> Create()
        {
            return new List<Project>
            {
                new Project
                {
                    Id = 1,

                    Title = "Pocket Forest",

                    ShortDescription =
                        "A smart terrarium monitoring and control system for managing multiple terrariums.",

                    Description =
                        "Pocket Forest is a smart terrarium control system developed as a four-person team project. " +
                        "The system combines physical hardware with a companion web application for monitoring " +
                        "environmental conditions, managing lighting schedules and controlling multiple terrariums.",

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
                        "Focused primarily on the web interface including the dashboard, temperature and humidity pages, " +
                        "lighting controls, journal, information pages, settings, navigation and terrarium management.",

                    SystemArchitecture =
                        "The ESP32 acts as the main controller for the user interface, configuration and web access. " +
                        "An Arduino Nano acts as a sub-controller for direct lighting and environmental sensor control. " +
                        "The system supports up to five terrarium lid modules.",

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
                        "Sunrise and sunset simulation",
                        "Terrarium journal",
                        "Photo Mode",
                        "Information pages",
                        "QR and network access"
                    },

                    Testing = new List<string>
                    {
                        "QR connectivity",
                        "Time-based lighting behaviour",
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
                    }
                },


                new Project
                {
                    Id = 2,

                    Title =
                        "Event Manager",

                    ShortDescription =
                        "An ASP.NET Core event-management web application.",

                    Description =
                        "A full-stack event management application for creating and managing events, " +
                        "registering attendees and providing real-time updates.",

                    Technologies =
                        "C#, ASP.NET Core MVC, Entity Framework Core, Identity, SignalR, Azure SQL",

                    ProjectType =
                        "Web Application",

                    Role =
                        "Full-Stack Development",

                    MyContribution =
                        "Developed the application using ASP.NET Core MVC including event and attendee management, " +
                        "database integration, authentication, role-based authorization and SignalR.",

                    SystemArchitecture =
                        "ASP.NET Core MVC with Entity Framework Core, ASP.NET Core Identity, SignalR and Azure SQL.",

                    Features = new List<string>
                    {
                        "Create and manage events",
                        "Attendee registration",
                        "Organizer and attendee roles",
                        "Authentication and authorization",
                        "Real-time SignalR updates",
                        "Cloud database integration"
                    }
                },


                new Project
                {
                    Id = 3,

                    Title =
                        "IoT Project",

                    ShortDescription =
                        "An IoT application using embedded hardware and Node-RED.",

                    Description =
                        "An IoT project developed using embedded systems, Node-RED and JavaScript.",

                    Technologies =
                        "Node-RED, JavaScript, IoT, Embedded Systems",

                    ProjectType =
                        "IoT Project"
                },


                new Project
                {
                    Id = 4,

                    Title =
                        "PC BUILDING",

                    ShortDescription =
                        "Custom gaming PCs built, configured, tested and troubleshot for customers.",

                    Description =
                        "A collection of custom gaming PCs independently assembled, configured, " +
                        "tested, troubleshot and prepared for customers.",

                    Technologies =
                        "PC Hardware, Windows, BIOS, Drivers, Diagnostics, Benchmarking",

                    ImageUrl =
                        "/images/pc/cover.png",

                    ProjectType =
                        "Independent Technical Work",

                    Role =
                        "PC Builder & Technician",

                    Team =
                        "Independent"
                }
            };
        }
    }
}