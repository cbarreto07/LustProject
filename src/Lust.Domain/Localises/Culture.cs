using Lust.Domain.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lust.Domain.Localises
{
    public class Culture : Entity
    {
        
        public string Name { get; set; }
        public virtual List<Resource> Resources { get; set; }
    }
}
