namespace FootballLeague.Controllers
{
    using FootballLeague.Models;
    using FootballLeague.Services.Interfaces;
    using FootballLeague.Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MatchesController : Controller
    {
        private readonly IMatchService service;
        private readonly ITeamService teamService;

        public MatchesController(IMatchService matchService, ITeamService teamService)
        {
            this.service = matchService;
            this.teamService = teamService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(service.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            MatchViewModel viewModel = new MatchViewModel();
            var teams = teamService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            viewModel.Teams = teams;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MatchViewModel match)
        {
            if (ModelState.IsValid && match.HomeTeamId != match.AwayTeamId)
            {
                service.CreateMatch(new MatchDTO
                {
                    HomeTeamId = match.HomeTeamId.Value,
                    AwayTeamId = match.AwayTeamId.Value,
                    HomeTeamGoals = match.HomeTeamGoals.Value,
                    AwayTeamGoals = match.AwayTeamGoals.Value,
                });
                return RedirectToAction(nameof(Index));
            }

            if (match.HomeTeamId == match.AwayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", "The team cannot play against itself");
            }

            var teams = teamService.GetAll()
             .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            match.Teams = teams;
            return View(match);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var match = service.GetById(id);
            MatchViewModel viewModel = new MatchViewModel()
            {
                Id = match.Id,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                HomeTeamGoals = match.HomeTeamGoals,
                AwayTeamGoals = match.AwayTeamGoals,
            };

            var teams = teamService.GetAll()
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            viewModel.Teams = teams;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MatchViewModel match)
        {
            if (ModelState.IsValid)
            {
                if (match.HomeTeamId == match.AwayTeamId)
                {
                    this.ModelState.AddModelError(nameof(match.HomeTeamId), "The team cannot play against itself.");
                    return View(match);
                }

                service.CreateMatch(new MatchDTO
                {
                    Id = match.Id,
                    HomeTeamId = match.HomeTeamId.Value,
                    AwayTeamId = match.AwayTeamId.Value,
                    HomeTeamGoals = match.HomeTeamGoals.Value,
                    AwayTeamGoals = match.AwayTeamGoals.Value,
                });
                return RedirectToAction(nameof(Index));
            }

            var teams = teamService.GetAll()
             .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            match.Teams = teams;
            return View(match);

        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View(service.GetById(int.Parse(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int matchId)
        {
            try
            {
                service.DeleteMatch(matchId);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }
    }
}
