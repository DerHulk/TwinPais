using System;
using System.Linq;
using System.Collections.Generic;

namespace TwinPairs.Core
{
    public class Game
    {
        private int MaxExposedCards = 2;
        private readonly List<History> History = new List<Core.History>();

        public Guid Id { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
        public virtual IEnumerable<Player> Players { get; set; }

        public Card SelectCard(Position position)
        {
            return this.Cards.SingleOrDefault(x => x.Position.Column == position.Column && 
                                                   x.Position.Row == position.Row);
        }

        public void AddHistory(History history)
        {
            History.Add(history);
        }

        public History[] GetLastHistory()
        {
            var result = new List<History>();
            var stack = new Stack<History>(this.History.OrderByDescending(x => x.Date));
            var lastPlayer = stack.Peek().Player;

            while (stack.Count > 0 && stack.Peek()?.Player == lastPlayer)
            {
                result.Add(stack.Pop());
            }

            return result.ToArray();
        }

        public Card[] GetCards(History[] history)
        {
            var cards = from h in history
                        join c in this.Cards on h.Exposed equals c.Position
                        select c;

            return cards.ToArray();
        }

        public bool IsPair(Card[] cards)
        {
            var expectedPairs = (float)cards.Count() / this.MaxExposedCards;
            var toSkip = (int)(Math.Ceiling(expectedPairs -1) * this.MaxExposedCards);
            var relevant = cards.Skip(toSkip).Distinct();

            if (!relevant.Any())
                return false;

            return relevant.Count() == this.MaxExposedCards && 
                   relevant.All(x => x.Motive == cards.FirstOrDefault().Motive);
        }

        public Player GetCurrentPlayer()
        {
            if (!this.History.Any())
                return this.Players.FirstOrDefault();

            var lastHistory = this.GetLastHistory();
            var exposeIsMissing = (lastHistory.Count() % this.MaxExposedCards != 0);
            var lastPlayer = lastHistory.First().Player;

            if (exposeIsMissing)
                return lastPlayer;

            var cards = this.GetCards(lastHistory);

            if (this.IsPair(cards))
                return lastPlayer;

            return this.GetNextPlayer(lastPlayer);
        }

        private Player GetNextPlayer(Player currentPlayer)
        {
            var playerList = this.Players.ToList();
            var index = playerList.IndexOf(currentPlayer);

            if (index == -1)
                throw new InvalidOperationException("Playerlist is corrupt");

            if (index + 1 >= this.Players.Count())
                return this.Players.First();
            else
                return playerList[index + 1];
        }

    }
}
