using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinPairs.Core;
using Xunit;

namespace TwinPairs.Core.Tests
{
    public class GameFactoryTests
    {
        [Fact(DisplayName ="Check that the result count is correct.")]
        public void Test01()
        {
            //arrange
            var creator = new Player();
            var factory = new GameFactory();
            var settings = new GameSettings() { Motives = new Motive[] { new Motive(1, "cat") ,
                                                                         new Motive(1, "dog") }};
            //act
            var result = factory.Create(settings, creator);

            //assert
            Assert.True(result.Cards.Count() == 4);
        }
    }
}
