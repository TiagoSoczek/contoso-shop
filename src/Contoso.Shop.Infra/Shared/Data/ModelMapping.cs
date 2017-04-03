using Contoso.Shop.Model.AccessControl;
using Contoso.Shop.Model.Catalog;
using Contoso.Shop.Model.Shared;
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
            Map(modelBuilder.Entity<Departament>());
        }

        private void Map(EntityTypeBuilder<Product> map)
        {
            map.Property(x => x.Title).IsRequired();
            map.Property(x => x.Sku).IsRequired();

            MapAudit(map);
        }

        private void Map(EntityTypeBuilder<Departament> map)
        {
            MapAudit(map);
        }

        private void MapAudit<T>(EntityTypeBuilder<T> map) where T : AuditedEntity
        {
            map.Property(x => x.CreatedAt).IsRequired();
            map.HasOne(x => x.CreatedBy);
            map.Property(x => x.CreatedById);

            map.Property(x => x.UpdatedAt).IsRequired(false);
            map.HasOne(x => x.UpdatedBy);
            map.Property(x => x.UpdatedById);
        }
    }
}