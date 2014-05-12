using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;

using Web.Models.Puzzle;
using System.Configuration;

namespace Web.Controllers
{
    public class PuzzleController : Controller
    {
        private readonly PuzzleService puzzleService;
        
        public PuzzleController()
        {
            puzzleService = new PuzzleService(ConfigurationManager.AppSettings["DatabaseName"]);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _List()
        {
            var model = puzzleService.GetLastTenPuzzles();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _Create(PuzleCreateViewModel model)
        {
            var puzzle = puzzleService.CreatePuzzle(model.BucketCapacity1, model.BucketCapacity2, model.DesiredAmount);
            puzzleService.SolvePuzzle(puzzle.Id);
            return RedirectToAction("_List");
        }

        public ActionResult _Solution(string id)
        {
            var model = puzzleService.Get(Guid.Parse(id));
            return PartialView(model);
        }
	}
}