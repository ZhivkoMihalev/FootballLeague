namespace FootballLeague.Services.Implementations
{
    using FootballLeague.DataAccess;
    using FootballLeague.Services.Interfaces;
    using FootballLeague.Services.Models;

    public class StandingService : IStandingService
    {
        private readonly FootballLeagueDbContext db;

        public StandingService(FootballLeagueDbContext _db)
        {
            this.db = _db;
        }

        public IEnumerable<StandingDTO> GetStanding()
        {
            var teamStandings = db.Teams.Select(x => new StandingDTO { TeamId = x.TeamId, Name = x.Name }).ToList();

            foreach (var match in this.db.Matches)
            {
                var homeTeam = teamStandings.Where(x => x.TeamId == match.HomeTeamId).First();
                var awayTeam = teamStandings.Where(x => x.TeamId == match.AwayTeamId).First();

                if (match.HomeTeamGoals > match.AwayTeamGoals)
                {
                    homeTeam.Wins++;
                    homeTeam.Points++;
                    homeTeam.GoalsFor += match.HomeTeamGoals;
                    homeTeam.GoalsAgainst += match.AwayTeamGoals;
                    homeTeam.Points += 3;

                    awayTeam.Losses++;
                    awayTeam.GoalsFor += match.AwayTeamGoals;
                    awayTeam.GoalsAgainst += match.HomeTeamGoals;
                }

                else if (match.HomeTeamGoals < match.AwayTeamGoals)
                {
                    homeTeam.Losses++;
                    homeTeam.GoalsFor += match.HomeTeamGoals;
                    homeTeam.GoalsAgainst += match.AwayTeamGoals;

                    awayTeam.Wins++;
                    awayTeam.GoalsFor += match.AwayTeamGoals;
                    awayTeam.GoalsAgainst += match.HomeTeamGoals;
                    awayTeam.Points++;
                }

                else if (match.HomeTeamGoals == match.AwayTeamGoals)
                {
                    homeTeam.Draws++;
                    homeTeam.GoalsFor += match.HomeTeamGoals;
                    homeTeam.GoalsAgainst += match.AwayTeamGoals;
                    homeTeam.Points++;

                    awayTeam.Draws++;
                    awayTeam.GoalsFor += match.AwayTeamGoals;
                    awayTeam.GoalsAgainst += match.HomeTeamGoals;
                    awayTeam.Points++;
                }
            }

            IEnumerable<StandingDTO> standingResult = teamStandings
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalsDifference)
                .ThenByDescending(t => t.GoalsFor)
                .ThenByDescending(t => t.Wins)
                .AsEnumerable();

            return standingResult;
        }
    }
}
