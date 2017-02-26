using System.ComponentModel.DataAnnotations;
using Contoso.Shop.Model.Catalog.Commands;

namespace Contoso.Shop.Api.Catalog.Dtos
{
    public class UpdateDepartamentDto : IUpdateDepartament
    {
        [Required]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string Description { get; set; }
    }
}