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
        private static readonly Dictionary<string, Player> Store = new Dictionary<string, Player>();

        public void CreatePlayer(ClaimsPrincipal identity)
        {
            var player = new Player() { Id = Guid.NewGuid(), Name = identity.Identity.Name };
            var id = this.GetId(identity);

            Store.Add(id, player);
        }

        public Player GetPlayer(string provider, string providerKey)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(ClaimsPrincipal identity)
        {
            var id = this.GetId(identity);

            if (!Store.ContainsKey(id))
                return null;

            return Store[id];
        }

        private string GetId(ClaimsPrincipal identity)
        {
            return identity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
        }
    }
}
