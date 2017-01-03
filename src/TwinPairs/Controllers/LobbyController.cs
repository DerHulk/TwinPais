using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace TwinPairs.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
       
        public JsonResult Create() {
            throw new NotImplementedException();
        }

        public JsonResult Join() {
            throw new NotImplementedException();
        }

        public JsonResult Start() {
            throw new NotImplementedException();
        }
    }
}
