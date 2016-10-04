using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    [System.Diagnostics.DebuggerDisplay("")]
    public class Round
    {
        public readonly int Number;
        private readonly List<Card> _ExposedCards = new List<Card>();
        
        public Player Player { get; }
        public DateTime Starts { get; private set; }
        public DateTime? Ends { get; private set; }
        public IEnumerable<Card> ExposedCards { get { return _ExposedCards; } }
        public int MaxExposing { get; }
        public Game Game { get; }

        public Round(Game game, Player current, int maxExposing)
        {
            this.Game = game;
            this.Starts = DateTime.Now;
            this.Player = current;
            this.MaxExposing = maxExposing;
        }

        public ExposeResult Expose(Card card)
        {
            if (!Game.Cards.Contains(card))
                throw new ArgumentOutOfRangeException(nameof(card));

            _ExposedCards.Add(card);

            var exposeIsMissing = this.ExposedCards.Count() % this.MaxExposing != 0;

            if (exposeIsMissing)
                return ExposeResult.MissinExposing;

            var nextPlayer = this.GetNextPlayer();
            return new ExposeResult(false, false, new Round(this.Game,nextPlayer, this.MaxExposing));
        }

        private Player GetNextPlayer()
        {
            var playerList = this.Game.Players.ToList();
            var index = playerList.IndexOf(this.Player);

            if (index == -1)
                throw new InvalidOperationException("Playerlist is corrupt");

            if (index + 1 >= this.Game.Players.Count())
                return this.Game.Players.First();
            else
                return playerList[index + 1];
        }

        
    }
}
