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
        private IGameStore GameStore { get; } = new GamesStore(); //hack

        [HttpGet("/game/read/{gameId}")]
        [Authorize]
        public ActionResult Read(Guid gameId)
        {
            var game = this.GameStore.LoadById(gameId);
            var model = game.Cards.ToArray();

            return this.Json(model);

        }

        [HttpGet("/game/expose/{gameId}/")]
        public JsonResult Expose(Guid gameId, int row, int column) {

            var game = this.GameStore.LoadById(gameId);
            var currentPlayer = game.GetCurrentPlayer();
            var selected = game.SelectCard(new Position(row, column));
            var result =  currentPlayer.Expose(selected, game);


            return this.Json(selected.Motive);
        }
    }
}
