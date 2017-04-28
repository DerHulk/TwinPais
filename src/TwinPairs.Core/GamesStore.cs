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
        void Save(Game selected);
    }

    public class GamesStore : IGameStore  
    {
        private SimplePersistence Persistence { get; } = new SimplePersistence();

        public Game LoadById(Guid id)
        {
            var games = this.ReadAll();

            return games?.SingleOrDefault(x => x.Id == id);
        }

        public Game[] LoadAllAvailableForPlayer(Player player)
        {
            var games = this.ReadAll();

            if (games == null)
                return new Game[] { };

            return games?.Where(x => x.State == GameStatus.WaitingForPlayers || 
                                    ((x.State == GameStatus.Running || 
                                      x.State == GameStatus.ReadyToStart) && 
                                      x.GetPlayers().Contains(player))).ToArray();
        }

        private Game[] ReadAll()
        {
            return Persistence.Read<GameState>()?.Select(x => Game.LoadFrom(x)).ToArray();
        }

        public void Save(Game game)
        {
            Persistence.Save(game.GetState(), x=> x.Id == game.Id);
        }
    }
}
