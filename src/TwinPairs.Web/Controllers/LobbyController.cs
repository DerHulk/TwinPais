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
    [Authorize]
    public class LobbyController : Controller
    {
        private IGameStore GameStore { get; } = new GamesStore(); //hack
        private IPlayerStore PlayerStore { get; } = new PlayerStore(); //hack

        [HttpGet]
        public JsonResult Index()
        {
            var player = this.PlayerStore.GetPlayer(this.User);
            return Json(this.GameStore.LoadAllAvailableForPlayer(player).Select(x => new
            {
                Id = x.Id,
                State = x.State,
                Players = x.GetPlayers().Select(p => p.Name)
            }).ToArray());
        }

        [HttpPost]
        public StatusCodeResult Create([FromBody]CreateGameCommandModel model)
        {
            if (model == null)
                return StatusCode((int)System.Net.HttpStatusCode.PreconditionFailed);

            var settings = new GameSettings();
            var motiveRepository = new MotiveRepository();
            var gameFactory = new GameFactory();

            settings.Motives = motiveRepository.LoadAll().Take(model.Cards);
            var creator = this.PlayerStore.GetPlayer(this.User);
            var game = gameFactory.Create(settings, creator);
            game.Id = Guid.NewGuid();

            this.GameStore.Save(game);

            return StatusCode(200);
        }

        [HttpPost]
        public ActionResult Join(string id)
        {
            Guid guidId = Guid.Empty;
            if (!Guid.TryParse(id, out guidId))
                return StatusCode((int)System.Net.HttpStatusCode.NotFound);

            var selected = this.GameStore.LoadById(guidId);
            var joiningPlayer = this.PlayerStore.GetPlayer(this.User);

            if (selected == null)
                return StatusCode((int)System.Net.HttpStatusCode.NotFound);

            if (!selected.CanJoin(joiningPlayer))
                return StatusCode((int)System.Net.HttpStatusCode.NotAcceptable);

            selected.AddPlayer(joiningPlayer);
            GameStore.Save(selected);

            return selected.State == GameStatus.ReadyToStart ?
                StatusCode((int)System.Net.HttpStatusCode.Created) :
                StatusCode((int)System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public JsonResult Start()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public JsonResult WhoIAm()
        {
            var youAre = this.PlayerStore.GetPlayer(this.User);

            return Json(youAre);
        }
    }
}
