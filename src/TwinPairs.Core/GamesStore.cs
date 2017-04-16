using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public interface IGameStore
    {
        Game LoadById(Guid id);
        Game[] LoadAllAvailableForPlayer(Player player);
        void Add(Game game);
    }

    public class GamesStore : IGameStore
    {
        private static List<Game> Games { get; } = new List<Game>();

        public Game LoadById(Guid id)
        {
            return Games.SingleOrDefault(g => g.Id == id);
        }

        public Game[] LoadAllAvailableForPlayer(Player player)
        {
            return Games.Where(x => x.State == GameStatus.WaitingForPlayers || 
                                    ((x.State == GameStatus.Running || 
                                      x.State == GameStatus.ReadyToStart) && 
                                      x.GetPlayers().Contains(player))).ToArray();
        }

        public void Add(Game game)
        {
            Games.Add(game);
        }
    }
}
