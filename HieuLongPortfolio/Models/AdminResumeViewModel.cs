namespace HieuLongPortfolio.Models
{
    public class AdminResumeViewModel
    {
        public bool CurrentResumeExists { get; set; }

        public IFormFile? ResumeFile { get; set; }


        // =========================================
        // WEB RESUME
        // =========================================

        public string ProfessionalSummary { get; set; } =
            string.Empty;


        // =========================================
        // EDUCATION
        // =========================================

        public string EducationProgram { get; set; } =
            string.Empty;

        public string EducationSchool { get; set; } =
            string.Empty;

        public string EducationLocation { get; set; } =
            string.Empty;

        public string EducationDate { get; set; } =
            string.Empty;


        // =========================================
        // EXPERIENCE
        // =========================================

        public List<AdminResumeExperienceViewModel> Experiences
        { get; set; } =
            new List<AdminResumeExperienceViewModel>();


        // =========================================
        // PROJECTS
        // =========================================

        public List<AdminResumeProjectViewModel> Projects
        { get; set; } =
            new List<AdminResumeProjectViewModel>();


        // =========================================
        // SKILLS
        // =========================================

        public string PcHardwareSupport { get; set; } =
            string.Empty;

        public string Languages { get; set; } =
            string.Empty;

        public string FrameworksTechnologies { get; set; } =
            string.Empty;

        public string Databases { get; set; } =
            string.Empty;

        public string CloudDeployment { get; set; } =
            string.Empty;

        public string EmbeddedRealTime { get; set; } =
            string.Empty;

        public string Tools { get; set; } =
            string.Empty;
    }


    public class AdminResumeExperienceViewModel
    {
        public string Title { get; set; } =
            string.Empty;

        public string Company { get; set; } =
            string.Empty;

        public string Location { get; set; } =
            string.Empty;

        public string Date { get; set; } =
            string.Empty;

        public string BulletsText { get; set; } =
            string.Empty;
    }


    public class AdminResumeProjectViewModel
    {
        public string Title { get; set; } =
            string.Empty;

        public string Technologies { get; set; } =
            string.Empty;

        public string BulletsText { get; set; } =
            string.Empty;
    }
}