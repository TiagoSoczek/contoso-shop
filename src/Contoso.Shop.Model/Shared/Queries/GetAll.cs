namespace Contoso.Shop.Model.Shared.Queries
{
    public sealed class GetAll<T> where T : Entity
    {
        internal static readonly GetAll<T> Instance = new GetAll<T>();

        private GetAll()
        {
        }
    }
}