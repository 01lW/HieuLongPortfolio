namespace HieuLongPortfolio.Models
{
    public class ResumeData
    {
        public string ProfessionalSummary { get; set; } = string.Empty;

        public ResumeEducation Education { get; set; } =
            new ResumeEducation();

        public List<ResumeExperience> Experiences { get; set; } =
            new List<ResumeExperience>();

        public List<ResumeProject> Projects { get; set; } =
            new List<ResumeProject>();

        public ResumeSkills Skills { get; set; } =
            new ResumeSkills();
    }


    public class ResumeEducation
    {
        public string Program { get; set; } = string.Empty;

        public string School { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;
    }


    public class ResumeExperience
    {
        public string Title { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public List<string> Bullets { get; set; } =
            new List<string>();
    }


    public class ResumeProject
    {
        public string Title { get; set; } = string.Empty;

        public string Technologies { get; set; } = string.Empty;

        public List<string> Bullets { get; set; } =
            new List<string>();
    }


    public class ResumeSkills
    {
        public List<string> PcHardwareSupport { get; set; } =
            new List<string>();

        public List<string> Languages { get; set; } =
            new List<string>();

        public List<string> FrameworksTechnologies { get; set; } =
            new List<string>();

        public List<string> Databases { get; set; } =
            new List<string>();

        public List<string> CloudDeployment { get; set; } =
            new List<string>();

        public List<string> EmbeddedRealTime { get; set; } =
            new List<string>();

        public List<string> Tools { get; set; } =
            new List<string>();
    }
}