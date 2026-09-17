using HieuLongPortfolio.Models;

namespace HieuLongPortfolio.Services
{
    public interface IResumeRepository
    {
        Task<ResumeData> GetAsync();

        Task SaveAsync(ResumeData resume);
    }
}