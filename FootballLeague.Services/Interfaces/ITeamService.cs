namespace FootballLeague.Services.Interfaces
{
    using FootballLeague.Services.Models;

    public interface ITeamService
    {
        IEnumerable<TeamDTO> GetAll();

        TeamDTO GetById(int teamId);

        void Add(string name);

        void Edit(int teamId, string name);

        void Delete(int teamId);
    }
}
