using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.AccessControl
{
    public static class Users
    {
        public static readonly User SystemAccount = User.Create(SystemAccountId, SystemAccountName);

        public const int SystemAccountId = 1;
        public const string SystemAccountName = "System";
    }

    public class User : Entity
    {
        private User()
        {
        }

        public string Name { get; private set; }

        public static User Create(int id, string name)
        {
            return new User
            {
                Id = id,
                Name = name
            };
        }
    }
}