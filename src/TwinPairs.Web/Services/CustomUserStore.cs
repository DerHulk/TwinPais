using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TwinPairs.Core;
using TwinPairs.Web.ViewModels;

namespace TwinPairs.Web.Services
{
    public class CustomUserStore<T> : IUserStore<T>, Microsoft.AspNetCore.Identity.IUserLoginStore<T> where T : ApplicationUser
    {
        private PlayerStore PlayerStore { get; } = new PlayerStore();

        public Task AddLoginAsync(T user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(T user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(T user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<T> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<T> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return Task.FromResult<T>(null);
        }

        public Task<T> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(T user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(T user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<string> GetUserIdAsync(T user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<string> GetUserNameAsync(T user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task RemoveLoginAsync(T user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task SetNormalizedUserNameAsync(T user, string normalizedName, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task SetUserNameAsync(T user, string userName, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<IdentityResult> UpdateAsync(T user, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
