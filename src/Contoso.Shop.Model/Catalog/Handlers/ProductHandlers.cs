using System.Threading.Tasks;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using Contoso.Shop.Model.Shared.Repositories;
using System.Collections.Generic;
using MediatR;

namespace Contoso.Shop.Model.Catalog.Handlers
{
    public class ProductHandlers : IAsyncRequestHandler<CreateProduct, Result<Product>>, 
                                   IAsyncRequestHandler<UpdateProduct, Result<Product>>,
                                   IAsyncRequestHandler<GetAll<Product>, IEnumerable<Product>>,
                                   IAsyncRequestHandler<GetById<Product>, Result<Product>>,
                                   IAsyncRequestHandler<RemoveCommand<Product>, Result>
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
        public async Task<Result<Product>> Handle(UpdateProduct command)
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

        public async Task<Result<Product>> Handle(CreateProduct command)
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

        private async Task<Result> Validate(CreateProduct command)
        {
            var existsWithSku = await repository.Exists(x => x.Sku == command.Sku);

            if (existsWithSku)
            {
                return CatalogResults.ProductAlreadyExistsWithSku;
            }

            var deptoExists = await departamentRepository.EnsureExists(command.DepartamentId);

            return deptoExists;
        }

        private async Task<Result> Validate(UpdateProduct command)
        {
            var deptExists = await departamentRepository.EnsureExists(command.DepartamentId);

            return deptExists;
        }
    }
}