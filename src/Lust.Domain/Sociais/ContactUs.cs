using Lust.Domain.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Lust.Domain.Sociais
{
    public class ContactUs : Entity
    {
        
        
        
        public string Name { get; set; }

        
        public string Email { get; set; }

        
        public string Message { get; set; }

    }

}
