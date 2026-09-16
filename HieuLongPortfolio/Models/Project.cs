namespace HieuLongPortfolio.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Technologies { get; set; } = string.Empty;

        public string Hardware { get; set; } = string.Empty;

        public string ProjectType { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Team { get; set; } = string.Empty;

        public string MyContribution { get; set; } = string.Empty;

        public string SystemArchitecture { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string GitHubUrl { get; set; } = string.Empty;

        public string DemoUrl { get; set; } = string.Empty;

        public List<string> GalleryImages { get; set; } = new();

        public List<string> Features { get; set; } = new();

        public List<string> Testing { get; set; } = new();

        public List<string> Challenges { get; set; } = new();
    }
}