using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TwinPairs.Core.Tests
{
    public class HashCombinerTests
    {
        [Fact]
        public void CombineHash1()
        {
            //arrange
            var someValues = new object[] { new int[] { } , new object(), new List<string>() };
            //act

            var result = HashCombiner.CombineHashCodes(someValues);

            //assert
            Assert.NotEqual(0, result);
        }
    }
}
