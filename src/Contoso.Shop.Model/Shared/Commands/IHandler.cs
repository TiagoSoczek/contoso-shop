using System.Threading.Tasks;

namespace Contoso.Shop.Model.Shared.Commands
{
    public interface IHandler<in T, TR>
    {
        Task<Result<TR>> Handle(T command);
    }
}