using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class HashCombiner
    {
        public static int CombineHashCodes(int h1, int h2)
        {
            return (((h1 << 5) + h1) ^ h2);
        }

        public static int CombineHashCodes(params object[] obj)
        {
            if (obj.Count() == 1)
                throw new InvalidOperationException("obj.Count() == 1");

            int resultHash = obj[0].GetHashCode();

            for (int i = 0; i < obj.Length; i++)
            {
                var hash = obj[i]?.GetHashCode();

                if(hash.HasValue)
                    resultHash = CombineHashCodes(resultHash, hash.Value);
            }

            return resultHash;
        }
    }
}
