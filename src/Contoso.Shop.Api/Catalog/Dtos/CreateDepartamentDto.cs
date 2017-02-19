using System.ComponentModel.DataAnnotations;
using Contoso.Shop.Model.Catalog.Commands;

namespace Contoso.Shop.Api.Catalog.Dtos
{
    public class CreateDepartamentDto : ICreateDepartament
    {
        [Required, MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string Description { get; set; }
    }
}