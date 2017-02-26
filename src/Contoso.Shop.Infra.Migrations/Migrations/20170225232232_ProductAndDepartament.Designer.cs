using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Contoso.Shop.Infra.Shared.Data;

namespace Contoso.Shop.Infra.Migrations.Migrations
{
    [DbContext(typeof(ShopDataContext))]
    [Migration("20170225232232_ProductAndDepartament")]
    partial class ProductAndDepartament
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Departament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Departament");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<int>("DepartamentId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("Sku")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Contoso.Shop.Model.Catalog.Product", b =>
                {
                    b.HasOne("Contoso.Shop.Model.Catalog.Departament", "Departament")
                        .WithMany()
                        .HasForeignKey("DepartamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
