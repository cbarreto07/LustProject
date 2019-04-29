using System;

using FluentValidation;
using Lust.Domain.Core.Extensions;

namespace Lust.Domain.Clientes.Validations
{
    public abstract class DoteValidation : AbstractValidator<Dote>
    {
        protected void ValidateDescricao()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Informe a descrição")
                .Length(2, 50).WithMessage("A descrição tem que ter pelo menos 2 e no máximo 50 caracteres");
        }

       
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

       
    }
}