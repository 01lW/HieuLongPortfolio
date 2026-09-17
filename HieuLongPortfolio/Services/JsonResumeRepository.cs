using HieuLongPortfolio.Models;
using System.Text.Json;

namespace HieuLongPortfolio.Services
{
    public class JsonResumeRepository : IResumeRepository
    {
        private readonly string _filePath;

        private readonly SemaphoreSlim _fileLock =
            new SemaphoreSlim(1, 1);


        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };


        public JsonResumeRepository(
            IWebHostEnvironment environment)
        {
            var dataDirectory =
                Path.Combine(
                    environment.ContentRootPath,
                    "App_Data");


            Directory.CreateDirectory(
                dataDirectory);


            _filePath =
                Path.Combine(
                    dataDirectory,
                    "resume.json");


            EnsureFileExists();
        }


        private void EnsureFileExists()
        {
            if (File.Exists(_filePath))
            {
                return;
            }


            var resume =
                ResumeSeedData.Create();


            var json =
                JsonSerializer.Serialize(
                    resume,
                    _options);


            File.WriteAllText(
                _filePath,
                json);
        }


        public async Task<ResumeData> GetAsync()
        {
            await _fileLock.WaitAsync();

            try
            {
                var json =
                    await File.ReadAllTextAsync(
                        _filePath);


                if (string.IsNullOrWhiteSpace(json))
                {
                    return ResumeSeedData.Create();
                }


                return JsonSerializer
                           .Deserialize<ResumeData>(
                               json,
                               _options)
                       ?? ResumeSeedData.Create();
            }
            finally
            {
                _fileLock.Release();
            }
        }


        public async Task SaveAsync(
            ResumeData resume)
        {
            await _fileLock.WaitAsync();

            try
            {
                var json =
                    JsonSerializer.Serialize(
                        resume,
                        _options);


                await File.WriteAllTextAsync(
                    _filePath,
                    json);
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}