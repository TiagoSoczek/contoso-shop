using System.ComponentModel.DataAnnotations;
using Contoso.Shop.Model.Catalog.Commands;

namespace Contoso.Shop.Api.Catalog.Dtos
{
    public class CreateProductDto : ICreateProduct
    {
        [Required, MinLength(3), MaxLength(30)]
        public string Sku { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Title { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string ShortDescription { get; set; }

        [Required, Range(0, 100000)]
        public decimal? Price { get; set; }

        [Required, Range(0, 100000)]
        public int? Quantity { get; set; }

        [Required]
        public int? DepartamentId { get; set; }
    }
}