using System.Threading.Tasks;
using Contoso.Shop.Model.Catalog.Commands;
using Contoso.Shop.Model.Shared;
using Contoso.Shop.Model.Shared.Commands;
using Contoso.Shop.Model.Shared.Repositories;

namespace Contoso.Shop.Model.Catalog.Handlers
{
    public class DepartamentHandlers : IHandler<ICreateDepartament, Departament>
    {
        private readonly IRepository<Departament> repository;

        public DepartamentHandlers(IRepository<Departament> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Departament>> Handle(ICreateDepartament command)
        {
            if (command == null)
            {
                throw Error.ArgumentNull(nameof(command));
            }

            var validationResult = Validate(command);

            if (validationResult.IsFailure)
            {
                return validationResult.As<Departament>();
            }

            var departament = new Departament
            {
                Title = command.Title,
                Description = command.Description,
                CreatedAt = Clock.Now
            };

            await repository.Insert(departament);

            return Result.Ok(departament);
        }

        private Result Validate(ICreateDepartament command)
        {
            return Result.Ok();
        }
    }
}