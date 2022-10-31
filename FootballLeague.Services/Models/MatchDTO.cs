namespace FootballLeague.Services.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MatchDTO
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number!")]
        public int HomeTeamGoals { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value must be a positive number!")]
        public int AwayTeamGoals { get; set; }

        public char? SignResult
        {
            get
            {
                if (this.HomeTeamGoals > this.AwayTeamGoals)
                    return '1';

                else if (this.HomeTeamGoals < this.AwayTeamGoals)
                    return '2';

                else if (this.HomeTeamGoals == this.AwayTeamGoals)
                    return 'X';

                else
                    return null;
            }
        }
    }
}
