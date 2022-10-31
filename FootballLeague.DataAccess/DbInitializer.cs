namespace FootballLeague.DataAccess
{
    using FootballLeague.DataAccess.Models;

    public static class DbInitializer
    {
        public static void Initialize(FootballLeagueDbContext context)
        {
            var check = context.Database.EnsureCreated();

            if (context.Teams.Any())
            {
                return;
            }

            var teams = new List<Team>()
            {
                new Team { Name = "ЦСКА тире София" },
                new Team { Name = "Liverpool" },
                new Team { Name = "Barcelona" },
                new Team { Name = "Levski Sofia" },
                new Team { Name = "Manchester City" },
                new Team { Name = "FC Milan" },
                new Team { Name = "PSG"},
                new Team { Name = "Chelsea"}
            };

            context.Teams.AddRange(teams);
            context.SaveChanges();

            Random random = new Random();
            var matches = new List<Match>();
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = 0; j < teams.Count; j++)
                {
                    if (i == j)
                        continue;

                    matches.Add(new Match { AwayTeamId = teams[i].TeamId, HomeTeamId = teams[j].TeamId, HomeTeamGoals = random.Next(0, 5), AwayTeamGoals = random.Next(0, 5) });
                }
            }

            context.Matches.AddRange(matches);
            context.SaveChanges();
        }
    }
}