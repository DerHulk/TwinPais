using System;
using System.Linq;
using System.Collections.Generic;

namespace TwinPairs.Core
{
    public class GameSettings
    {
        public int CardsCount { get { return this.Motives.Count() * 2; } }
        public IEnumerable<Motive> Motives { get; set; }

        public IEnumerable<Position> GetPositions()
        {
            var result = new List<Position>();

            for (int r = 0; r < this.Motives.Count(); r++)
            {
                for (int c = 0; c < this.Motives.Count(); c++)
                {
                    result.Add(new Position() { Row = r, Column = c });
                }
            }

            return result;
        }

    }
}
