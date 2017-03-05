using Contoso.Shop.Model.Shared;
using FluentValidation;
using MediatR;

namespace Contoso.Shop.Model.Catalog.Commands
{
    public class CreateDepartament : IRequest<Result<Departament>>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public class Validator : AbstractValidator<CreateDepartament>
        {
            public Validator()
            {
                RuleFor(x => x.Title).NotEmpty().Length(3, 50);
                RuleFor(x => x.Description).NotEmpty().Length(3, 100);
            }
        }
    }
}