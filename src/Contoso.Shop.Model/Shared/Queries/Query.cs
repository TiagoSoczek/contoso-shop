namespace Contoso.Shop.Model.Shared.Queries
{
    public static class Query
    {
        public static GetAll<T> All<T>() where T : Entity
        {
            return GetAll<T>.Instance;
        }

        public static GetById<T> ById<T>(int id) where T : Entity
        {
            return new GetById<T>(id);
        }
    }
}