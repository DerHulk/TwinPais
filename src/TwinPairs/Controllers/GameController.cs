using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using TwinPairs.Core;
using TwinPairs.ViewModels;

namespace TwinPairs.Controllers
{

    public class GameController : Controller
    {
        private static Game CurrentGame;

        [HttpGet()]
        [Authorize]
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
                game.Players = new Player[] { new Player() { Id = Guid.Parse("0f80a756-ba96-4f8a-8333-cbc8f9ef372d"),
                                                             Name = "Tom" },
                                              new Player() { Id = Guid.Parse("7fd6885f-d2fe-46ce-b7c1-fb48026a6a60"),
                                                             Name = "Lilu" } };

                CurrentGame = game; ;
            }
            return this.View();
        }

        public JsonResult Expose(int row, int column) {

            var currentPlayer = CurrentGame.GetCurrentPlayer();
            var selected = CurrentGame.SelectCard(new Position(row, column));
            var result =  currentPlayer.Expose(selected, CurrentGame);


            return this.Json(selected.Motive);
        }
    }
}
