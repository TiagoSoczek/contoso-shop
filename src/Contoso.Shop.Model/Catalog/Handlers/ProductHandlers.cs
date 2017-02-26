using System.Threading.Tasks;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using Contoso.Shop.Model.Shared.Repositories;
using System.Collections.Generic;

namespace Contoso.Shop.Model.Catalog.Handlers
{
    public class ProductHandlers
    {
        private readonly IRepository<Product> repository;
        private readonly IRepository<Departament> departamentRepository;

        public ProductHandlers(IRepository<Product> repository, IRepository<Departament> departamentRepository)
        {
            this.repository = repository;
            this.departamentRepository = departamentRepository;
        }

        public Task<IEnumerable<Product>> Handle(GetAll<Product> query)
        {
            if (query == null)
            {
                throw Error.ArgumentNull(nameof(query));
            }

            return repository.GetAll();
        }

        public Task<Result> Handle(RemoveCommand<Product> command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
            }

            return repository.Delete(command.Id);
        }

        public Task<Result<Product>> Handle(GetById<Product> query)
        {
            if (query == null)
            {
                throw Error.ArgumentNull(nameof(query));
            }

            return repository.GetById(query.Id);
        }
        public async Task<Result<Product>> Handle(IUpdateProduct command)
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

            var productResult = await repository.GetById(command.Id);

            if (productResult.IsFailure)
            {
                return productResult;
            }

            var product = productResult.Value;

            product.Apply(command);

            await repository.Update(product);

            return Result.Ok(product);
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

            var product = Product.Create(command);

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

            var deptoExists = await departamentRepository.EnsureExists(command.DepartamentId);

            return deptoExists;
        }

        private async Task<Result> Validate(IUpdateProduct command)
        {
            var deptExists = await departamentRepository.EnsureExists(command.DepartamentId);

            return deptExists;
        }
    }
}