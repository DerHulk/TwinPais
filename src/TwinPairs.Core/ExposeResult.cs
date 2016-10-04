using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class ExposeResult
    {
        public static readonly ExposeResult MissinExposing = new ExposeResult(false, true);

        public bool IsPair { get; private set; }
        public bool CanExposeFuther { get; private set; }
        public bool RoundHasEnd { get { return !this.CanExposeFuther; } }
        public Round NextRound { get; private set; }

        public ExposeResult(bool isPair, bool canExposeFuther )
        {
            this.IsPair = IsPair;
            this.CanExposeFuther = canExposeFuther;
        }

        public ExposeResult(bool isPair, bool canExposeFuther, Round nextRound) 
            : this(isPair, canExposeFuther)
        {
            this.NextRound = nextRound;
        }
    }
}
