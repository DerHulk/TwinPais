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
        private static GameModel CurrentGame;

        [HttpGet()]
        public ActionResult Index()
        {
            //hack
            if (CurrentGame == null)
            {
                var settings = new GameSettings();
                var motiveRepository = new MotiveRepository();
                var gameFactory = new GameFactory();

                settings.Motives = motiveRepository.LoadAll();
                var game = gameFactory.Create(settings);
                CurrentGame = new GameModel(game);
            }

            var model = CurrentGame;
            return this.View(model);
        }

        public JsonResult Expose(int row, int column) {
            return this.Json(13);
        }
    }
}
