namespace FootballLeague.Services.Interfaces
{
    using FootballLeague.Services.Models;
    using System.Collections.Generic;

    public interface IMatchService
    {
        IEnumerable<MatchDTO> GetAll();

        MatchDTO GetById(int matchId);

        void CreateMatch(MatchDTO match);

        void EditMatch(MatchDTO match);

        void DeleteMatch(int id);
    }
}
