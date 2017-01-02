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

        public ExposeResult(bool isPair, bool canExposeFuther )
        {
            this.IsPair = IsPair;
            this.CanExposeFuther = canExposeFuther;
        }
    }
}
