using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Queries;
using Contoso.Shop.Model.Shared.Repositories;
using MediatR;

namespace Contoso.Shop.Model.Catalog.Handlers
{
    public class DepartamentHandlers : IAsyncRequestHandler<CreateDepartament, Result<Departament>>,
                                   IAsyncRequestHandler<UpdateDepartament, Result<Departament>>,
                                   IAsyncRequestHandler<GetAll<Departament>, IEnumerable<Departament>>,
                                   IAsyncRequestHandler<GetById<Departament>, Result<Departament>>,
                                   IAsyncRequestHandler<RemoveCommand<Departament>, Result>
    {
        private readonly IRepository<Departament> repository;

        public DepartamentHandlers(IRepository<Departament> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Departament>> Handle(CreateDepartament command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
            }

            var departament = Departament.Create(command);

            await repository.Insert(departament);

            return Result.Ok(departament);
        }

        public Task<IEnumerable<Departament>> Handle(GetAll<Departament> query)
        {
            if (query == null)
            {
                throw Error.ArgumentNull(nameof(query));
            }

            return repository.GetAll();
        }

        public Task<Result> Handle(RemoveCommand<Departament> command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
            }

            return repository.Delete(command.Id);
        }

        public Task<Result<Departament>> Handle(GetById<Departament> query)
        {
            if (query == null)
            {
                throw Error.ArgumentNull(nameof(query));
            }

            return repository.GetById(query.Id);
        }

        public async Task<Result<Departament>> Handle(UpdateDepartament command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
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
    }
}