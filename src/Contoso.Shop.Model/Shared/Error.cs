using System;

namespace Contoso.Shop.Model.Shared
{
    public static class Error
    {
        public static ArgumentNullException ArgumentNull(string param) => new ArgumentNullException(param);
    }
}