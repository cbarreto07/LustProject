



namespace Lust.Domain.Clientes.Validations
{
    public class UpdateClienteCommandValidation :  ClienteValidation
    {
        public UpdateClienteCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateEmail();
            ValidadeCpf();
            ValidadeCep();
        }
    }
}