using System;
using System.Linq;
using System.Collections.Generic;


namespace TwinPairs.Core
{
    public class MotiveRepository
    {
        public IEnumerable<Motive> LoadAll()
        {
            return new Motive[] 
            {
                new Motive(1,"Bird"),
                new Motive(2, "Dog"),
                new Motive(3, "Sheep"),
                new Motive(4, "Cat"),
                new Motive(5, "Turtel"),
            };
        }
    }
}
