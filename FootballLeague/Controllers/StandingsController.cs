namespace FootballLeague.Controllers
{
    using FootballLeague.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class StandingsController : Controller
    {
        private readonly IStandingService service;

        public StandingsController(IStandingService standingService)
        {
            this.service = standingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(service.GetStanding());
        }
    }
}
