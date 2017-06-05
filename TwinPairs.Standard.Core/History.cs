using System;
using System.Collections.Generic;
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
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (exposed == null)
                throw new ArgumentNullException(nameof(player));

            if (date == DateTime.MinValue || DateTime.MaxValue == date)
                throw new ArgumentOutOfRangeException(nameof(date));

            this.Player = player;
            this.Exposed = exposed;
            this.Date = date;
        }
    }
}
