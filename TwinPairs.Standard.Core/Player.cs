
using System;

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
            if (card == null)
                throw new ArgumentNullException(nameof(card));
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            var history = new History(this, card.Position, DateTime.Now);
            game.AddHistory(history);

            return card.Expose().Motive;
        }
    }
}
