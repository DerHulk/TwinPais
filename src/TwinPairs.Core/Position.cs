
using System.Diagnostics.Contracts;

namespace TwinPairs.Core
{
    [System.Diagnostics.DebuggerDisplay("Row {Row} Column {Column}")]
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            Contract.Requires(row > 0);
            Contract.Requires(column > 0);

            this.Row = row;
            this.Column = column;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Position);
        }

        public bool Equals(Position obj)
        {
            if (obj == null)
                return false;

            return int.Equals(obj.Row, this.Row) && int.Equals(obj.Column, this.Column);
        }

        public override int GetHashCode()
        {
            return HashCombiner.CombineHashCodes(this.Row, this.Column);
        }
    }
}
