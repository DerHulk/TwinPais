using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class Game
    {
        public Guid Id { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
