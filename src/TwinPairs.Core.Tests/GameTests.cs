using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TwinPairs.Core.Tests
{
    public class GameTests
    {
        private readonly Game Target;

        public GameTests()
        {
            this.Target =  new Game();
            this.Target.State = GameStatus.WaitingForPlayers;
            this.Target.AddPlayer(new Player() { Name = "Tom" });
            this.Target.AddPlayer(new Player() { Name = "Lilu" });
            this.Target.Cards = new Card[]
            {
                new Card() { Position = new Position(1,1), Motive = new Motive(1,"cat") },
                new Card() { Position = new Position(2,1) , Motive = new Motive(1,"dog")  },
                new Card() { Position = new Position(3,1), Motive = new Motive(1,"dog")  },
                new Card() { Position = new Position(4,1), Motive = new Motive(1,"cat")  },
            }.Select(x=> new MaskedCard(x));
        }

        [Fact(DisplayName = "Proof if we get the correct card.")]
        public void SelectCard01()
        {
            //arrange
            var expected = new MaskedCard( new Card() { Position = new Position(1, 2) });
            Target.Cards = Target.Cards.Union(new MaskedCard[] { expected });

            //act
            var card = Target.SelectCard(new Position(1, 2));
            //assert
            Assert.Equal(expected, card);
        }

        [Fact(DisplayName = "Proof if we get the first player.")]
        public void GetCurrentPlayer01()
        {
            //arrange
            var expected = this.Target.GetPlayers().First();

            //act
            var result = this.Target.GetCurrentPlayer();

            //assert
            Assert.Equal(expected, result);
        }

        [Theory(DisplayName ="Proof if the given cards represent one or more pairs")]
        [InlineData(1, false)]
        [InlineData(2,true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        public void IsPair01(int cardsCount, bool expected)
        {
            //arrange
            var motiv = new Motive(1,"dog");
            var cards = new List<Card>();

            for (int i = 0; i < cardsCount; i++)
            {
                cards.Add(new Card() { Motive = motiv, Position = new Position(i,1) });
            }

            //act
            var result =  this.Target.IsPair(cards.ToArray());

            //assert
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName ="Ensures that expose the same card not is equal")]
        public void IsPair02()
        {
            //arrange
            var cards = new List<Card>();
            cards.Add(new Card() { Motive = new Motive(1, "Dog"), Position = new Position(1, 1) });
            cards.Add(new Card() { Motive = new Motive(1, "Dog"), Position = new Position(1, 1) });

            //act
            var result = this.Target.IsPair(cards.ToArray());

            //assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Proof if we get the next player after the first is finished.")]
        public void GetCurrentPlayer02()
        {
            //arrange
            this.Target.State = GameStatus.Running;
            var firstPlayer = this.Target.GetCurrentPlayer();
            var card = this.Target.SelectCard(new Position(1, 1));
            firstPlayer.Expose(card, this.Target);
            firstPlayer.Expose(card, this.Target);

            //act
            var result = this.Target.GetCurrentPlayer();

            //assert
            Assert.NotEqual(firstPlayer, result);
        }
    }
}
