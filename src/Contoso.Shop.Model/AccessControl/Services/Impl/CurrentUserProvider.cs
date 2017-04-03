using System.Threading.Tasks;

namespace Contoso.Shop.Model.AccessControl.Services.Impl
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public Task<User> GetCurrentUser()
        {
            return Task.FromResult(Users.SystemAccount);
        }

        public Task<int> GetCurrentUserId()
        {
            return Task.FromResult(Users.SystemAccountId);
        }
    }
}