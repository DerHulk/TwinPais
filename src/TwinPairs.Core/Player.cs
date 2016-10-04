
using System;

namespace TwinPairs.Core
{
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public class Player
    {
        public Guid Id { get; set; }
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

    }
}
