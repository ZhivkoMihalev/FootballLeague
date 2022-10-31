namespace FootballLeague.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class MatchViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Home team")]
        public int? HomeTeamId { get; set; }

        [Required]
        [Display(Name = "Away team")]
        public int? AwayTeamId { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "The value must be a positive number!")]
        [Display(Name = "Home team goals")]
        public int? HomeTeamGoals { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "The value must be a positive number!")]
        [Display(Name = "Away team goals")]
        public int? AwayTeamGoals { get; set; }

        public List<SelectListItem>? Teams { get; set; }
    }
}
