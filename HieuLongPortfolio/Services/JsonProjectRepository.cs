using HieuLongPortfolio.Models;
using System.Text.Json;

namespace HieuLongPortfolio.Services
{
    public class JsonProjectRepository : IProjectRepository
    {
        private readonly string _filePath;

        private readonly SemaphoreSlim _fileLock =
            new SemaphoreSlim(1, 1);


        private readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };


        public JsonProjectRepository(
            IWebHostEnvironment environment)
        {
            var dataDirectory = Path.Combine(
                environment.ContentRootPath,
                "App_Data");


            Directory.CreateDirectory(dataDirectory);


            _filePath = Path.Combine(
                dataDirectory,
                "projects.json");


            EnsureFileExists();
        }


        // =========================================
        // INITIAL FILE
        // =========================================

        private void EnsureFileExists()
        {
            if (File.Exists(_filePath))
            {
                return;
            }


            var projects =
                ProjectSeedData.Create();


            var json =
                JsonSerializer.Serialize(
                    projects,
                    _jsonOptions);


            File.WriteAllText(
                _filePath,
                json);
        }


        // =========================================
        // GET ALL
        // =========================================

        public async Task<IReadOnlyList<Project>>
            GetAllAsync()
        {
            await _fileLock.WaitAsync();

            try
            {
                var projects =
                    await ReadProjectsAsync();


                return projects
                    .OrderBy(project => project.Id)
                    .ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }


        // =========================================
        // GET BY ID
        // =========================================

        public async Task<Project?>
            GetByIdAsync(int id)
        {
            await _fileLock.WaitAsync();

            try
            {
                var projects =
                    await ReadProjectsAsync();


                return projects
                    .FirstOrDefault(
                        project => project.Id == id);
            }
            finally
            {
                _fileLock.Release();
            }
        }


        // =========================================
        // CREATE
        // =========================================

        public async Task<Project>
            CreateAsync(Project project)
        {
            await _fileLock.WaitAsync();

            try
            {
                var projects =
                    await ReadProjectsAsync();


                project.Id =
                    projects.Count == 0
                        ? 1
                        : projects.Max(p => p.Id) + 1;


                projects.Add(project);


                await WriteProjectsAsync(projects);


                return project;
            }
            finally
            {
                _fileLock.Release();
            }
        }


        // =========================================
        // UPDATE
        // =========================================

        public async Task<bool>
            UpdateAsync(Project project)
        {
            await _fileLock.WaitAsync();

            try
            {
                var projects =
                    await ReadProjectsAsync();


                var index =
                    projects.FindIndex(
                        existing =>
                            existing.Id == project.Id);


                if (index < 0)
                {
                    return false;
                }


                projects[index] = project;


                await WriteProjectsAsync(projects);


                return true;
            }
            finally
            {
                _fileLock.Release();
            }
        }


        // =========================================
        // DELETE
        // =========================================

        public async Task<bool>
            DeleteAsync(int id)
        {
            await _fileLock.WaitAsync();

            try
            {
                var projects =
                    await ReadProjectsAsync();


                var project =
                    projects.FirstOrDefault(
                        p => p.Id == id);


                if (project == null)
                {
                    return false;
                }


                projects.Remove(project);


                await WriteProjectsAsync(projects);


                return true;
            }
            finally
            {
                _fileLock.Release();
            }
        }


        // =========================================
        // READ JSON
        // =========================================

        private async Task<List<Project>>
            ReadProjectsAsync()
        {
            var json =
                await File.ReadAllTextAsync(
                    _filePath);


            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Project>();
            }


            return JsonSerializer.Deserialize<List<Project>>(
                       json,
                       _jsonOptions)
                   ?? new List<Project>();
        }


        // =========================================
        // WRITE JSON
        // =========================================

        private async Task
            WriteProjectsAsync(
                List<Project> projects)
        {
            var json =
                JsonSerializer.Serialize(
                    projects,
                    _jsonOptions);


            await File.WriteAllTextAsync(
                _filePath,
                json);
        }
    }
}