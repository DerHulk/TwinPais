using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwinPairs.Core;
using TwinPairs.Web.ViewModels;

namespace TwinPairs.Controllers
{
    public class TemplateController : Controller
    {
        [HttpGet()]
        [Route("Template/Board")]
        public PartialViewResult GetBoard()
        {
            return PartialView("Board");
        }

        [HttpGet()]
        [Route("Template/Lobby")]
        public PartialViewResult GetLobby()
        {
            return PartialView("Lobby");
        }
    }
}
