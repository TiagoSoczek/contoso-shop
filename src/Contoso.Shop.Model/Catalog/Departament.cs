using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Departament : AuditedEntity
    {
        private Departament()
        {
        }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public static Departament Create(CreateDepartament command)
        {
            return new Departament
            {
                Title = command.Title,
                Description = command.Description
            };
        }

        public void Apply(UpdateDepartament command)
        {
            Title = command.Title;
            Description = command.Description;
        }
    }
}