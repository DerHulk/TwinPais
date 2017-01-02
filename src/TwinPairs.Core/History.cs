using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class History
    {
        public Player Player { get;  }
        public Position Exposed {get; }
        public DateTime Date { get; }

        public History(Player player, Position exposed, DateTime date)
        {
            Contract.Requires(player != null);
            Contract.Requires(exposed != null);
            Contract.Requires(date != DateTime.MinValue);
            Contract.Requires(date != DateTime.MaxValue);

            this.Player = player;
            this.Exposed = exposed;
            this.Date = date;
        }
    }
}
