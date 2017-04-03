using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Contoso.Shop.Infra.Shared.Data;

namespace Contoso.Shop.Infra.Migrations.Migrations
{
    [DbContext(typeof(ShopDataContext))]
    partial class ShopDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Contoso.Shop.Model.AccessControl.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Departament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<int?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Departaments");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<int?>("CreatedById");

                    b.Property<int>("DepartamentId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Sku")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<int?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DepartamentId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Departament", b =>
                {
                    b.HasOne("Contoso.Shop.Model.AccessControl.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Contoso.Shop.Model.AccessControl.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Product", b =>
                {
                    b.HasOne("Contoso.Shop.Model.AccessControl.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Contoso.Shop.Model.Catalog.Departament", "Departament")
                        .WithMany()
                        .HasForeignKey("DepartamentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Contoso.Shop.Model.AccessControl.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });
        }
    }
}
