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

            int resultHash = obj[1].GetHashCode();

            for (int i = 1; i < obj.Length; i++)
            {
                resultHash = CombineHashCodes(resultHash, obj[i]?.GetHashCode());
            }

            return resultHash;
        }
    }
}
