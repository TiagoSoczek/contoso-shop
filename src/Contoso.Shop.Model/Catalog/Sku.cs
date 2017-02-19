using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public struct Sku
    {
        public Sku(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw Error.ArgumentNull(nameof(code));
            }

            Code = code;
        }

        public string Code { get; }

        public static implicit operator Sku(string code) => new Sku(code);
        public static implicit operator string(Sku sku) => sku.Code;
    }
}