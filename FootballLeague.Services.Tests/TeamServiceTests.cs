using FootballLeague.DataAccess;
using FootballLeague.Services.Implementations;
using Moq;
using System.Data.Common;

namespace FootballLeague.Services.Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        [Test]
        public void GetAllShouldReturnAllTeams()
        {
            string result;
            var db = new Mock<FootballLeagueDbContext>();
            db.Setup(x => x.Teams).Callback((string a) => { result = a; });
            var teams = new TeamService(db.Object);
            teams.Add("Levski");
            Assert.AreEqual(teams.GetAll(), result);
        }
    }
}
