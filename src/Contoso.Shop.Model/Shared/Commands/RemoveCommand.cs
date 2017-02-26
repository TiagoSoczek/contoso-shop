namespace Contoso.Shop.Model.Shared.Commands
{
    public sealed class RemoveCommand<T> where T : Entity
    {
        internal RemoveCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}