
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
    }
}
