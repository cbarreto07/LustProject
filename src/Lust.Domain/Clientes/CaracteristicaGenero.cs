using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class CaracteristicaGenero
    {
        public Guid CaracteristicaId { get; set; }
        public EnumGenero Genero { get; set; }

        public Caracteristica Caracteristica { get; set; }
    }
}
