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
        private readonly int MaxExposing = 2;

        public RoundTests()
        {
            this.Game = new Moq.Mock<Core.Game>();
            this.Game.Setup(x => x.Players).Returns(new Player[] { new Player() {Name = "Hans" },
                                                                   new Player() { Name = "Scott" } });

            
            this.Target = new Round(this.Game.Object, this.Game.Object.Players.First(), this.MaxExposing);
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

        [Fact(DisplayName ="Checks if we get an Missing Exposing if we make only one exposing.")]
        public void Expose01()
        {
            //arrange
            var cardToExpose = new Card();
            this.Game.Setup(x => x.Cards).Returns(new Card[] { cardToExpose });
            
            //act
            var result = this.Target.Expose(cardToExpose);

            //assert
            Assert.Equal(ExposeResult.MissinExposing, result);
        }

        [Fact(DisplayName = "Checks if we get an argumentout of range if the card is not in the game.")]
        public void Expose02()
        {
            //arrange
            var cardToExpose = new Card();

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(()=> this.Target.Expose(cardToExpose));
        }

        [Fact(DisplayName = "Proofs if a round reached the max exposed cards without pair the round ist ending. And the next player is on the round.")]
        public void Expose03()
        {
            //arrange
            var cards = new Card[] {
                new Card() { Motive = new Motive() { Id = 1 } },
                new Card() { Motive = new Motive() { Id = 2 } },
                new Card() { Motive = new Motive() { Id = 3 } },
            };

            this.Game.Setup(x => x.Cards).Returns(cards);

            //act 
            ExposeResult result = null;

            for (int i = 0; i < MaxExposing; i++)
            {
                result = this.Target.Expose(cards.Skip(i).Take(1).First());
            }

            //assert
            Assert.NotNull(result);
            Assert.True(result.RoundHasEnd);
            Assert.NotNull(result.NextRound);
            Assert.NotEqual(Game.Object.Players.First(), result.NextRound.Player);
        }
    }
}
