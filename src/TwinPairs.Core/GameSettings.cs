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
            var square = (int) Math.Sqrt(this.CardsCount);
            var row = 0;
            var column = 0;


            for (int i = 0; i < this.CardsCount; i++)
            {
                if (column >= square)
                {
                    row++;
                    column = 0;
                }

                result.Add(new Position(row, column ));

                column++;
            }

            return result;
        }

        public IEnumerable<Position> GetRandomPositions()
        {
            var randomized = this.GetPositions().ToList();
            randomized.Shuffle();

            return randomized;
        }

    }
}
