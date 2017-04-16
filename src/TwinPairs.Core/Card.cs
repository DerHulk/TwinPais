using System;

namespace TwinPairs.Core
{
    public class Card
    {
        public Position Position { get; set; }
        public Motive Motive { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCombiner.CombineHashCodes(this.Position?.GetHashCode(),
                                                 this.Motive?.GetHashCode());
        }
    }

    public class MaskedCard
    {
        private Card Card { get; }
        public Position Position {
            get {
                return this.Card.Position;
            }
        }

        public MaskedCard(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            this.Card = card;
        }

        public Card Expose()
        {
            return this.Card;
        }
    }
}
