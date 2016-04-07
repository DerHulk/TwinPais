using System;
using System.Linq;
using System.Collections.Generic;

namespace TwinPairs.Core
{
    public class Game
    {
        public Guid Id { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
