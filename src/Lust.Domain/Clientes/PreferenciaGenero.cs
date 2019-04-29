using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class PreferenciaGenero
    {
        public Guid PreferenciaId { get; set; }
        public EnumGenero Genero { get; set; }

        public Preferencia Preferencia { get; set; }
    }
}
