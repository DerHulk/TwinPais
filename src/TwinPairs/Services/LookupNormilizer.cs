using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TwinPairs.Services
{
    public class LookupNormilizer : ILookupNormalizer
    {
        public string Normalize(string key)
        {
            return key;
        }
    }
}
