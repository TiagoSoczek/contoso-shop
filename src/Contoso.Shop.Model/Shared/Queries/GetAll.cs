using System.Collections.Generic;
using MediatR;

namespace Contoso.Shop.Model.Shared.Queries
{
    public sealed class GetAll<T> : IRequest<IEnumerable<T>> where T : Entity
    {
        internal static readonly GetAll<T> Instance = new GetAll<T>();

        private GetAll()
        {
        }
    }
}