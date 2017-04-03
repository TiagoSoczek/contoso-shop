using System.Threading.Tasks;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.AccessControl.Services.Impl
{
    public class AuditService : IAuditService
    {
        private readonly ICurrentUserProvider currentUserProvider;

        public AuditService(ICurrentUserProvider currentUserProvider)
        {
            this.currentUserProvider = currentUserProvider;
        }

        public async Task RegisterNew(AuditedEntity entity)
        {
            var user = await currentUserProvider.GetCurrentUser();

            entity.MarkAsNew(user);
        }

        public async Task RegisterUpdate(AuditedEntity entity)
        {
            var user = await currentUserProvider.GetCurrentUser();

            entity.MarkAsUpdated(user);
        }
    }
}