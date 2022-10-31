using FootballLeague.DataAccess;
using FootballLeague.Middlewares;
using FootballLeague.Services.Implementations;
using FootballLeague.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<FootballLeagueDbContext>(options =>
                      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        RegistereServices(builder);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

        //builder.Host.ConfigureLogging(logging =>
        //{
        //    logging.ClearProviders();
        //    logging.AddConsole();
        //});

        var app = builder.Build();
        CreateDbIfNotExists(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }
    private static void CreateDbIfNotExists(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<FootballLeagueDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

    private static void RegistereServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ITeamService, TeamService>();
        builder.Services.AddTransient<IMatchService, MatchService>();
        builder.Services.AddTransient<IStandingService, StandingService>();
    }
}