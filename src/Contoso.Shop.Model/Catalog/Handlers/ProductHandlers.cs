using System.Threading.Tasks;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Repositories;

namespace Contoso.Shop.Model.Catalog.Handlers
{
    public class ProductHandlers : IHandler<ICreateProduct, Product>
    {
        private readonly IRepository<Product> repository;

        public ProductHandlers(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Product>> Handle(ICreateProduct command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
            }

            var validationResult = await Validate(command);

            if (validationResult.IsFailure)
            {
                return validationResult.As<Product>();
            }

            var product = new Product
            {
                Sku = command.Sku,
                DepartamentId = command.DepartamentId.GetValueOrDefault(),
                Price = command.Price.GetValueOrDefault(),
                Quantity = command.Quantity.GetValueOrDefault(),
                ShortDescription = command.ShortDescription,
                Title = command.Title,
                CreatedAt = Clock.Now
            };

            await repository.Insert(product);

            return Result.Ok(product);
        }

        private async Task<Result> Validate(ICreateProduct command)
        {
            var existsWithSku = await repository.Exists(x => x.Sku == command.Sku);

            if (existsWithSku)
            {
                return CatalogResults.ProductAlreadyExistsWithSku;
            }

            return Result.Ok();
        }
    }
}