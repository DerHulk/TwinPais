using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Core
{
    public class PlayerStore : IPlayerStore
    {
        private SimplePersistence Persitence { get; } = new SimplePersistence(); 

        public void CreatePlayer(ClaimsPrincipal identity)
        {
            var id = this.GetId(identity);
            var player = new Player() { Id = id, Name = identity.Identity.Name,  };
            
            Persitence.Save(player, x=> x.Id == id);
        }

        public Player GetPlayer(string provider, string providerKey)
        {
            var players = this.Persitence.Read<Player>();
            return players?.SingleOrDefault(x => x.Id == providerKey);
        }

        public Player GetPlayer(ClaimsPrincipal identity)
        {
            var id = this.GetId(identity);
            var players = this.Persitence.Read<Player>();

            return players?.SingleOrDefault(x => x.Id == id);

        }

        private string GetId(ClaimsPrincipal identity)
        {
            return identity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
        }
    }
}
