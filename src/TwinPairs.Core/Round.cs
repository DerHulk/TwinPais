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

        public void Expose(Card card)
        {
            _ExposedCards.Add(card);

            if (!this.HasEnd() && this.ExposedCards.Count() >= MaxExposing)
                this.Ends = DateTime.Now;
        }

        public bool HasEnd()
        {
            return this.Ends.HasValue;
        }

        public bool IsPair() {

            if (!this.ExposedCards.Any())
                return false;

            return this.ExposedCards.All(x => 
                this.ExposedCards.First().Motive.Equals(x.Motive));
        }

        public Round NextRound()
        {
            var nextPlayer = this.IsPair() ? this.Player : this.GetNexPlayer();
            var newRound = new Round( this.Game, nextPlayer , MaxExposing);



            return newRound;
        }

        private Player GetNexPlayer()
        {
            var playerList = this.Game.Players.ToList();
            var playerIndex = playerList.IndexOf(this.Player);

            playerIndex++;

            if (playerIndex > playerList.Count)
                playerIndex = 0;

           return playerList[playerIndex];
        }
    }
}
