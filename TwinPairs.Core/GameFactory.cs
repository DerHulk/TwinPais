using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class GameFactory
    {
        public Game Create(GameSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var game = new Game();
            var availableMotives = new Queue<Motive>(settings.Motives);
            var positions = new Queue<Position>(settings.GetPositions());
            var cardSet = new List<Card>();

            for (int i = 0; i < settings.PairsCount; i++)
            {
                var motiv = availableMotives.Dequeue();
                var pairA = new Card()
                {
                    Motive = motiv,
                    Position = positions.Dequeue()
                };

                var pairB = new Card()
                {
                    Motive = motiv,
                    Position = positions.Dequeue()
                };

                cardSet.Add(pairA);
                cardSet.Add(pairB);
            }

            game.Cards = cardSet;
            return game;
        }
    }
}
