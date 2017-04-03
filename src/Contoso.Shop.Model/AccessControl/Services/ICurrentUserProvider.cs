using System.Threading.Tasks;

namespace Contoso.Shop.Model.AccessControl.Services
{
    public interface ICurrentUserProvider
    {
        Task<User> GetCurrentUser();
        
        Task<int> GetCurrentUserId();
    }
}