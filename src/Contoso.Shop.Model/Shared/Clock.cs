using System;

namespace Contoso.Shop.Model.Shared
{
    public static class Clock
    {
        public static DateTimeOffset Now => DateTimeOffset.Now;
        public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}