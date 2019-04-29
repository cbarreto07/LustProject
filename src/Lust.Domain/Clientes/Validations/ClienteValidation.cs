using System;

using FluentValidation;
using Lust.Domain.Core.Extensions;

namespace Lust.Domain.Clientes.Validations
{
    public abstract class ClienteValidation : AbstractValidator<Cliente>
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o seu nome ou apelido")
                .Length(2, 250).WithMessage("O seu nome tem que ter pelo menos 2 e no máximo 250 caracteres");
        }

        protected void ValidadeCpf()
        {
            RuleFor(c => c.Cpf)
                .CpfValido().WithMessage("Este CPF não é válido");
        }

        protected void ValidadeCep()
        {
            RuleFor(c => c.Cep)
                .CepValido().WithMessage("Este CEP não é válido");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.DataNascimento)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("Você precisa ter pelo menos 18 anos");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}