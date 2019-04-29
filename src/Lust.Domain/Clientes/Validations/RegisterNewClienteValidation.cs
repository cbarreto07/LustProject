



namespace Lust.Domain.Clientes.Validations
{
    public class RegisterNewClienteValidation : ClienteValidation
    {
        public RegisterNewClienteValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidadeCpf();
            ValidadeCep();
        }
    }
}