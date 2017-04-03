namespace Contoso.Shop.Model.Shared.Commands
{
    public static class Remove
    {
        public static RemoveCommand<T> For<T>(int id) where T : Entity
        {
            return new RemoveCommand<T>(id);
        }
    }
}