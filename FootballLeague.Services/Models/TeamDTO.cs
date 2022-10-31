namespace FootballLeague.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeamDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The teamname must be between 3 and 30 symbols.")]
        public string Name { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalsDifference => GoalsFor - GoalsAgainst;

        public int Points { get; set; }
    }
}
