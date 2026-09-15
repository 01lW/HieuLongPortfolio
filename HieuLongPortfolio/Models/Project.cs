namespace HieuLongPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Technologies { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string GitHubUrl { get; set; } = string.Empty;

        public string DemoUrl { get; set; } = string.Empty;
    }
}