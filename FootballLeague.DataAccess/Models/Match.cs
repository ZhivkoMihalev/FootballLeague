namespace FootballLeague.DataAccess.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public char? SignResult { get; set; }
    }
}
