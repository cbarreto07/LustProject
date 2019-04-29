using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{ 
    public class DoteCaracteristica
    {
        public Guid CaracteristicaId { get; set; }
        public Guid DoteId { get; set; }

        public virtual Caracteristica Caracteristica { get; set; }
        public virtual Dote Dote { get; set; }
    }
}
