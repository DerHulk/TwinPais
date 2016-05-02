using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwinPairs.Core;

namespace TwinPairs.ViewModels
{
    public class GameModel
    {
        private readonly Game Game;

        public GameModel(Game game)
        {
            this.Game = game;
            this.Rows = game.Cards.Max(x => x.Position.Row);
            this.Columns = game.Cards.Max(x => x.Position.Column);
            this.RowSize = 12 / (this.Columns + 1);
        }

        public int Rows { get; }
        public int Columns { get; }
        public int RowSize { get; }

        public Card GetCard(int row, int column)
        {
            return this.Game.Cards.Where(x => x.Position.Row == row &&
                                             x.Position.Column == column)
                                             .SingleOrDefault();
        }
    }
}
