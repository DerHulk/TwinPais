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
        public HttpStatusCodeResult Create([FromBody]CreateGameCommandModel model)
        {
            if (model == null)
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.PreconditionFailed);

            var settings = new GameSettings();
            var motiveRepository = new MotiveRepository();
            var gameFactory = new GameFactory();

            settings.Motives = motiveRepository.LoadAll().Take(model.Cards);
            var creator = this.PlayerStore.GetPlayer(this.User);
            var game = gameFactory.Create(settings, creator);
            game.Id = Guid.NewGuid();

            this.GameStore.Add(game);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult Join(string id)
        {
            Guid guidId = Guid.Empty;
            if (!Guid.TryParse(id, out guidId))
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.NotFound);

            var selected = this.GameStore.LoadById(guidId);
            var joiningPlayer = this.PlayerStore.GetPlayer(this.User);

            if (selected == null)
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.NotFound);

            if (!selected.CanJoin(joiningPlayer))
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.NotAcceptable);

            selected.AddPlayer(joiningPlayer);

            return selected.State == GameStatus.ReadyToStart ? 
                new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Created) :
                new HttpStatusCodeResult((int)System.Net.HttpStatusCode.OK);
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
