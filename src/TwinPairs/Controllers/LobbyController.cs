﻿using System;
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
        private static List<Game>  Games { get;  } = new List<Game>();

        [HttpGet]
        public JsonResult Index()
        {
            return Json(Games.Where(x=> x.Players.Count() <= 2).Select(x => new
                { Id = x.Id,
                  Player = x.Players.Select(p => p.Name) }
                ).ToArray());
        }

        [HttpPost]
        public HttpStatusCodeResult Create([FromBody]CreateGameCommandModel model)
        {
            if (model == null)
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.PreconditionFailed);

            var settings = new GameSettings();
            var motiveRepository = new MotiveRepository();
            var gameFactory = new GameFactory();

            settings.Motives = motiveRepository.LoadAll();
            var game = gameFactory.Create(settings);
            game.Id = Guid.NewGuid();
            game.Players = new Player[] { new Player() { Id = Guid.Parse("0f80a756-ba96-4f8a-8333-cbc8f9ef372d"),
                                                             Name = this.HttpContext.User.Identity.Name }};

            Games.Add(game);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult Join(string id)
        {
            Guid guidId = Guid.Empty;
            if (!Guid.TryParse(id, out guidId))
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.NotFound);

            var selected = Games.SingleOrDefault(x => x.Id == guidId);

            if (selected.Players.Count() >= 2 || selected.Players.Any(x=> x.Name == this.HttpContext.User.Identity.Name))
                return new HttpStatusCodeResult((int)System.Net.HttpStatusCode.NotAcceptable);

            var joiningPlayer = new Player()
            {
                Id = Guid.NewGuid(),
                Name = this.HttpContext.User.Identity.Name
            };

            selected.Players = selected.Players.Union(new Player[] { joiningPlayer });

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public JsonResult Start()
        {
            throw new NotImplementedException();
        }
    }
}
