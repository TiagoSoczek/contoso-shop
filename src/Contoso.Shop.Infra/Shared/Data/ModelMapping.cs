using Contoso.Shop.Model.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contoso.Shop.Infra.Shared.Data
{
    public class ModelMapping
    {
        private readonly ModelBuilder modelBuilder;

        public ModelMapping(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Map()
        {
            Map(modelBuilder.Entity<Product>());
        }

        private void Map(EntityTypeBuilder<Product> map)
        {
            map.Property(x => x.Title).IsRequired();
            map.Property(x => x.Sku).IsRequired();
        }
    }
}