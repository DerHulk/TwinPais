using System;
using System.Linq;
using System.Collections.Generic;

namespace TwinPairs.Core
{
    public enum GameStatus
    {
        Initalized = 0,
        WaitingForPlayers = 1,
        ReadyToStart = 2,
        Running = 4,
        Finished = 8,
    }

    public class Game
    {
        private int MaxExposedCards = 2;
        private readonly List<History> History = new List<Core.History>();

        public Guid Id { get; set; }
        public GameStatus State { get; internal set; }
        public virtual IEnumerable<MaskedCard> Cards { get; set; }
        private Dictionary<Player, List<Card>> PlayerList { get; set; } = new Dictionary<Player, List<Card>>();

        public void Initalize(Card[] cards)
        {

        }

        public bool CanJoin(Player player)
        {
            return (this.State == GameStatus.WaitingForPlayers ||
                    this.State == GameStatus.ReadyToStart) && 
                   !this.PlayerList.Keys.Any(x => x.Name == player.Name) &&
                    this.PlayerList.Keys.Count < 2;
        }

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            if (this.State != GameStatus.WaitingForPlayers && this.State != GameStatus.ReadyToStart)
                throw new InvalidOperationException();

            if (this.PlayerList.Keys.Contains(player))
                return;

            this.PlayerList.Add(player, new List<Card>());

            if (this.PlayerList.Count > 1)
                this.State = GameStatus.ReadyToStart;
        }

        public MaskedCard SelectCard(Position position)
        {
            return this.Cards.SingleOrDefault(x => x.Position.Column == position.Column && 
                                                   x.Position.Row == position.Row);
        }

        public void AddHistory(History history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            if (this.State == GameStatus.ReadyToStart && !this.History.Any())
                this.State = GameStatus.Running;

            if (this.State != GameStatus.Running)
                throw new NotSupportedException();

            History.Add(history);

            var lastHistory = this.GetLastHistory();
            if (!this.IsExposeMissing(lastHistory))
            {
                var cards = this.GetCards(lastHistory);

                if (this.IsPair(cards))
                {
                    this.PlayerList[history.Player].AddRange(cards);
                }
            }
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
                        select c.Expose();

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

        public Player[] GetPlayers()
        {
            return this.PlayerList.Keys.ToArray();
        }

        public Player GetCurrentPlayer()
        {
            if (!this.History.Any())
                return this.PlayerList.Keys.FirstOrDefault();

            var lastHistory = this.GetLastHistory();
            var lastPlayer = lastHistory.First().Player;

            if (this.IsExposeMissing(lastHistory))
                return lastPlayer;

            var cards = this.GetCards(lastHistory);

            if (this.IsPair(cards))
                return lastPlayer;

            return this.GetNextPlayer(lastPlayer);
        }

        private bool IsExposeMissing(History[] lastHistory)
        {
            return (lastHistory.Count() % this.MaxExposedCards != 0);
        }

        private Player GetNextPlayer(Player currentPlayer)
        {
            var playerList = this.PlayerList.Keys.ToList();
            var index = playerList.IndexOf(currentPlayer);

            if (index == -1)
                throw new InvalidOperationException("Playerlist is corrupt");

            if (index + 1 >= playerList.Count())
                return playerList.First();
            else
                return playerList[index + 1];
        }

    }
}
