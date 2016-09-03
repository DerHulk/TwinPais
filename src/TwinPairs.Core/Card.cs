
namespace TwinPairs.Core
{
    public class Card
    {
        public Position Position { get; set; }
        public Motive Motive { get; set; }
        public bool IsExposed { get; set; }
        public Player OwnedBy { get; set; }

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
}
