using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class Preferencia
    {
        public Guid Id { get; set; }

        

        public virtual ICollection<PreferenciaGenero> PrefereGeneros { get; set; }

        public Decimal Distancia { get; set; }

        public int IdadeMinima { get; set; }

        public int IdadeMaxima { get; set; }

        public decimal PrecoMinimo { get; set; }

        public decimal PrecoMaximo{ get; set; }

        public Cliente Cliente { get; set; }
    }
}
