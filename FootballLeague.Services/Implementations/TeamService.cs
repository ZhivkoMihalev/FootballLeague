namespace FootballLeague.Services.Implementations
{
    using FootballLeague.DataAccess;
    using FootballLeague.DataAccess.Models;
    using FootballLeague.Services.Interfaces;
    using FootballLeague.Services.Models;

    public class TeamService : ITeamService
    {
        private readonly FootballLeagueDbContext db;

        public TeamService(FootballLeagueDbContext _db)
        {
            this.db = _db;
        }

        public IEnumerable<TeamDTO> GetAll()
        {
            return db.Teams.Select(x => new TeamDTO { Id = x.TeamId, Name = x.Name });
        }

        public TeamDTO GetById(int teamId)
        {
            return db.Teams
                .Where(x => x.TeamId == teamId)
                .Select(x => new TeamDTO { Id = x.TeamId, Name = x.Name })
                .First();
        }

        public void Add(string name)
        {
            db.Add(new Team() { Name = name });
            db.SaveChanges();
        }

        public void Edit(int teamId, string name)
        {
            var team = db.Teams.Find(teamId);

            if (team == null)
                return;

            team.Name = name;
            db.SaveChanges();
        }

        public void Delete(int teamId)
        {
            var team = this.db.Teams.Find(teamId);

            if (team == null)
                return;

            if (this.db.Matches.Any(x => x.HomeTeamId == teamId) || this.db.Matches.Any(x => x.AwayTeamId == teamId))
            {
                throw new InvalidOperationException($"Cannot delete {team.Name}, because it is already participating in a match");
            }

            db.Teams.Remove(team);
            db.SaveChanges();
        }
    }
}