using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPairs.ViewModels
{
    public class CreateGameCommandModel
    {
        public int Cards { get; set; }
        public bool IsPublic { get; set; }
    }
}
