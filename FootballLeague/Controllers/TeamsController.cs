namespace FootballLeague.Controllers
{
    using FootballLeague.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    
    public class TeamsController : Controller
    {
        private readonly ITeamService service;

        public TeamsController(ITeamService teamService)
        {
            service = teamService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(service.GetById(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name)
        {
            try
            {
                service.Add(name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(service.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string name)
        {
            try
            {
                service.Edit(id, name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(service.GetById(int.Parse(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
