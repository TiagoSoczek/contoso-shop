using System;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Departament : Entity
    {
        private Departament()
        {
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        public static Departament Create(ICreateDepartament command)
        {
            return new Departament
            {
                Title = command.Title,
                Description = command.Description,
                CreatedAt = Clock.Now
            };
        }

        public void Apply(IUpdateDepartament command)
        {
            Title = command.Title;
            Description = command.Description;
            UpdatedAt = Clock.Now;
        }
    }
}