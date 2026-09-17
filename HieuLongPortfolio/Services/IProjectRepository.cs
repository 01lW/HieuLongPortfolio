using HieuLongPortfolio.Models;

namespace HieuLongPortfolio.Services
{
    public interface IProjectRepository
    {
        Task<IReadOnlyList<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int id);

        Task<Project> CreateAsync(Project project);

        Task<bool> UpdateAsync(Project project);

        Task<bool> DeleteAsync(int id);
    }
}