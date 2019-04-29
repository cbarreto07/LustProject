



namespace Lust.Domain.Clientes.Validations
{
    public class CreateDoteCommandValidation :  DoteValidation
    {
        public CreateDoteCommandValidation()
        {
            
            ValidateDescricao();
        }
    }
}