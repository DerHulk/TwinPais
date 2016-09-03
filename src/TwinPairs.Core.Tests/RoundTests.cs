using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TwinPairs.Core.Tests
{
    public class RoundTests
    {
        private readonly Round Target;
        private readonly Moq.Mock<Game> Game;
        private readonly Moq.Mock<Player> Player;
        private readonly int MaxExposing = 2;

        public RoundTests()
        {
            this.Game = new Moq.Mock<Core.Game>();
            this.Player = new Moq.Mock<Core.Player>();
            this.Target = new Round(this.Game.Object, this.Player.Object, this.MaxExposing);
        }

        [Fact]
        public void ctor01()
        {
            //arrange
            var game = new Game();
            var player = new Player();
            var maxExposing = 2;

            //act
            var target = new Round(game, player, maxExposing);

            //assert
            Assert.Equal(game, target.Game);
            Assert.Equal(player, target.Player);
            Assert.Equal(maxExposing, target.MaxExposing);

        }

        [Fact(DisplayName ="Proof that a empty round doesnt have a pair.")]
        public void IsPair01()
        {
            //arrange

            //act
            var result = this.Target.IsPair();

            //assert
            Assert.False(result);
        }

        [Fact(DisplayName ="Proofs if a new round is not direclty ended.")]
        public void HasEnd01()
        {
            //arrange

            //act
            var result = this.Target.HasEnd();

            //assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Proofs if a round reached the max exposed cards without pair the round ist ending.")]
        public void HasEnd02()
        {
            //arrange
            var cards = new Card[] {
                new Card() { Motive = new Motive() { Id = 1 } },
                new Card() { Motive = new Motive() { Id = 2 } },
                new Card() { Motive = new Motive() { Id = 3 } },
            };

            this.Game.Setup(x => x.Cards).Returns(cards);

            //act
            for (int i = 0; i < MaxExposing ; i++)
            {
                this.Target.Expose(cards.Skip(i).Take(1).First());
            }

            var result = this.Target.HasEnd();

            //assert
            Assert.True(result);
        }

    }
}
