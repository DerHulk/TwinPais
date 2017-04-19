using System.Security.Claims;

namespace TwinPairs.Core
{
    public interface IPlayerStore
    {
        void CreatePlayer(ClaimsPrincipal identity);
        Player GetPlayer(ClaimsPrincipal identity);
        Player GetPlayer(string provider, string providerKey);
    }
}