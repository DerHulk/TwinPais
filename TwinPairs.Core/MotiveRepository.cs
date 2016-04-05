﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class MotiveRepository
    {
        public IEnumerable<Motive> LoadAll()
        {
            return new Motive[] 
            {
                new Motive() { Id = 1, Name = "Bird" },
                new Motive() { Id = 2, Name = "Dog" },
                new Motive() { Id = 3, Name = "Sheep" },
                new Motive() { Id = 4, Name = "Cat" },
                new Motive() { Id = 5, Name = "Turtel" },
            };
        }
    }
}
