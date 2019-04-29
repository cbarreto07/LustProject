using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class Dote : Entity
    {
        
        public string Descricao { get; set; }


        public virtual ICollection<DoteCaracteristica> Caracteristicas { get; set; }

    }
}
