using System;
using Contoso.Shop.Model.Shared;

namespace Contoso.Shop.Model.Catalog
{
    public class Departament : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}