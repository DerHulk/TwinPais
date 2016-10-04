using System;
using System.Linq;
using System.Collections.Generic;

namespace TwinPairs.Core
{
    public class Game
    {
        private int MaxExposedCards = 2;
        private readonly Stack<Round> Rounds = new Stack<Round>();

        public Guid Id { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
        public virtual IEnumerable<Player> Players { get; set; }

        private Round StartFirstRound()
        {
            return new Round(this, this.Players.First(), MaxExposedCards);
        }

        public IEnumerable<Round> GetPlayedRounds() {
            return this.Rounds.Where(x => x.Ends.HasValue).OrderBy(x=> x.Starts).ToArray();
        }

        public bool IsFinished()
        {
            return this.Rounds.SelectMany(x => x.ExposedCards).Count() == this.Cards.Count();
        }

    }
}
