



namespace Lust.Domain.Clientes.Validations
{
    public class UpdateDoteCommandValidation :  DoteValidation
    {
        public UpdateDoteCommandValidation()
        {
            ValidateId();
            ValidateDescricao();
        }
    }
}