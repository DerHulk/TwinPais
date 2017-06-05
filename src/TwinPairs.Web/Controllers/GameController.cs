using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwinPairs.Core;
using TwinPairs.Web.ViewModels;

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


            if (game.State != GameStatus.ReadyToStart && game.State != GameStatus.Running)
                return StatusCode((int)System.Net.HttpStatusCode.PreconditionFailed);

            var model = game.Cards.ToArray();

            return this.Json(model);
        }

        [HttpGet("/game/expose/{gameId}/")]
        public ActionResult Expose(Guid gameId, int row, int column) {

            var game = this.GameStore.LoadById(gameId);
            var currentPlayer = game.GetCurrentPlayer();

            if(currentPlayer.Name != this.HttpContext.User.Identity.Name)
                return StatusCode((int)System.Net.HttpStatusCode.PreconditionFailed);

            var selected = game.SelectCard(new Position(row, column));
            var motiv = currentPlayer.Expose(selected, game);

            this.GameStore.Save(game);

            return this.Json(motiv);
        }
    }
}
