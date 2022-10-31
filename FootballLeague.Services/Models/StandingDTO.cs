namespace FootballLeague.Services.Models
{
    public class StandingDTO
    {
        public int TeamId { get; set; }

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
