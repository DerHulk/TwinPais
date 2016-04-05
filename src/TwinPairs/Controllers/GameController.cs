using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TwinPairs.Core;

namespace TwinPairs.Controllers
{
    public class GameController
    {
        [HttpGet()]
        public ActionResult Index()
        {
            var settings = new GameSettings();
            var motiveRepository = new MotiveRepository();
            var gameFactory = new GameFactory();

            settings.Motives = motiveRepository.LoadAll();
            var game = gameFactory.Create(settings);

            return new HttpOkResult();
        }
    }
}
