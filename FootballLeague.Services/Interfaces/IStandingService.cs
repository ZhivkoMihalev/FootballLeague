namespace FootballLeague.Services.Interfaces
{
    using FootballLeague.Services.Models;

    public interface IStandingService
    {
        IEnumerable<StandingDTO> GetStanding();
    }
}
