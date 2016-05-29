using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TwinPairs.Core;
using TwinPairs.ViewModels;

namespace TwinPairs.Controllers
{
    public class GameController : Controller
    {
        [HttpGet()]
        public ActionResult Index()
        {
            var settings = new GameSettings();
            var motiveRepository = new MotiveRepository();
            var gameFactory = new GameFactory();

            settings.Motives = motiveRepository.LoadAll();
            var game = gameFactory.Create(settings);
            var model = new GameModel(game);

            return this.View(model);
        }

        public ActionResult Angular()
        {
            return View();
        }
    }
}
