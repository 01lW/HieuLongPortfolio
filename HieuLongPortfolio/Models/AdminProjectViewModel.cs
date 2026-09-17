using System.ComponentModel.DataAnnotations;

namespace HieuLongPortfolio.Models
{
    public class AdminProjectViewModel
    {
        public int Id { get; set; }


        // REQUIRED
        [Required]
        [Display(Name = "Project Title")]
        public string Title { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Full Description")]
        public string Description { get; set; } = string.Empty;


        // OPTIONAL
        public string? Technologies { get; set; }

        public string? Hardware { get; set; }


        [Display(Name = "Project Type")]
        public string? ProjectType { get; set; }


        public string? Role { get; set; }

        public string? Team { get; set; }


        [Display(Name = "My Contribution")]
        public string? MyContribution { get; set; }


        [Display(Name = "System Architecture")]
        public string? SystemArchitecture { get; set; }


        [Display(Name = "Cover Image")]
        public string? ImageUrl { get; set; }


        [Display(Name = "GitHub URL")]
        public string? GitHubUrl { get; set; }


        [Display(Name = "Demo URL")]
        public string? DemoUrl { get; set; }


        [Display(Name = "Gallery Images")]
        public string? GalleryImagesText { get; set; }


        [Display(Name = "Features")]
        public string? FeaturesText { get; set; }


        [Display(Name = "Testing")]
        public string? TestingText { get; set; }


        [Display(Name = "Challenges")]
        public string? ChallengesText { get; set; }
    }
}