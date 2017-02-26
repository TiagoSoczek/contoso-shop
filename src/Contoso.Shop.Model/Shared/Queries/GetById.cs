namespace Contoso.Shop.Model.Shared.Queries
{
    public sealed class GetById<T> where T : Entity
    {
        internal GetById(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}