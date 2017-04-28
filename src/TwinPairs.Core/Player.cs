
using System;
using System.Diagnostics.Contracts;

namespace TwinPairs.Core
{
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Player;

            if (toCompare == null)
                return false;
            else
                return toCompare.Id == this?.Id && toCompare?.Name == this.Name;
        }

        public override int GetHashCode()
        {
            return HashCombiner.CombineHashCodes(this.Id, this.Name);
        }

        public Motive Expose(MaskedCard card, Game game)
        {
            Contract.Requires(card != null);
            Contract.Requires(game != null);

            var history = new History(this, card.Position, DateTime.Now);
            game.AddHistory(history);

            return card.Expose().Motive;
        }
    }
}
