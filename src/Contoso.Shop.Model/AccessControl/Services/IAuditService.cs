using System.Threading.Tasks;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.AccessControl.Services
{
    public interface IAuditService
    {
        Task RegisterNew(AuditedEntity entity);
        Task RegisterUpdate(AuditedEntity entity);
    }
}