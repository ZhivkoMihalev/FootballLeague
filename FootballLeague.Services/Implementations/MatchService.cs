namespace FootballLeague.Services.Implementations
{
    using FootballLeague.DataAccess;
    using FootballLeague.DataAccess.Models;
    using FootballLeague.Services.Interfaces;
    using FootballLeague.Services.Models;

    public class MatchService : IMatchService
    {
        private readonly FootballLeagueDbContext db;

        public MatchService(FootballLeagueDbContext _db)
        {
            this.db = _db;
        }

        public void CreateMatch(MatchDTO match)
        {
            var newMatch = new Match()
            {
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                HomeTeamGoals = match.HomeTeamGoals,
                AwayTeamGoals = match.AwayTeamGoals,
            };

            db.Matches.Add(newMatch);
            db.SaveChanges();
        }

        public void DeleteMatch(int id)
        {
            var currentMatch = db.Matches.FirstOrDefault(x => x.MatchId == id);
            if (currentMatch == null)
                throw new ArgumentNullException($"It doesnt exist a match with this ID.");

            db.Matches.Remove(currentMatch);
            db.SaveChanges();
        }

        public void EditMatch(MatchDTO match)
        {
            var currentMatch = db.Matches.FirstOrDefault(m => m.MatchId == match.Id);
            if (currentMatch == null)
                throw new ArgumentNullException("It doesnt have any match with this ID.");

            currentMatch.HomeTeamId = match.HomeTeamId;
            currentMatch.AwayTeamId = match.AwayTeamId;
            currentMatch.HomeTeamGoals = match.HomeTeamGoals;
            currentMatch.AwayTeamGoals = match.AwayTeamGoals;
            db.SaveChanges();
        }

        public IEnumerable<MatchDTO> GetAll()
        {
            return (from match in db.Matches
                    join homeTeam in db.Teams on match.HomeTeamId equals homeTeam.TeamId
                    join awayTeam in db.Teams on match.AwayTeamId equals awayTeam.TeamId
                    select new MatchDTO
                    {
                        Id = match.MatchId,
                        HomeTeamId = match.HomeTeamId,
                        AwayTeamId = match.AwayTeamId,
                        HomeTeamGoals = match.HomeTeamGoals,
                        AwayTeamGoals = match.AwayTeamGoals,
                        HomeTeamName = homeTeam.Name,
                        AwayTeamName = awayTeam.Name,
                    }).AsEnumerable();
        }

        public MatchDTO GetById(int matchId)
        {
            var currentMatch = (from match in db.Matches
                                join homeTeam in db.Teams on match.HomeTeamId equals homeTeam.TeamId
                                join awayTeam in db.Teams on match.AwayTeamId equals awayTeam.TeamId
                                where match.MatchId == matchId
                                select new MatchDTO
                                {
                                    Id = match.MatchId,
                                    HomeTeamId = match.HomeTeamId,
                                    AwayTeamId = match.AwayTeamId,
                                    HomeTeamGoals = match.HomeTeamGoals,
                                    AwayTeamGoals = match.AwayTeamGoals,
                                    HomeTeamName = homeTeam.Name,
                                    AwayTeamName = awayTeam.Name,
                                }).FirstOrDefault();

            if (currentMatch == null)
            {
                throw new ArgumentNullException("It doesnt have any match with this id!");
            }

            return currentMatch;
        }
    }
}
