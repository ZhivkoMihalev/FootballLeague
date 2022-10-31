namespace FootballLeague.DataAccess
{
    using FootballLeague.DataAccess.Models;
    using Microsoft.EntityFrameworkCore;

    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext()
        {
        }

        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Match>()
                .ToTable("Match")
                .HasOne(x => x.HomeTeam).WithMany()
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Match>()
                .ToTable("Match")
                .HasOne(x => x.AwayTeam)
                .WithMany()
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
