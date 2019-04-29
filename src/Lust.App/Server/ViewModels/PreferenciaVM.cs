using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class PreferenciaVM
    {
        public Guid Id { get; set; }

        public Decimal Distancia { get; set; }

        public int IdadeMinima { get; set; }

        public int IdadeMaxima { get; set; }

        public decimal PrecoMinimo { get; set; }

        public decimal PrecoMaximo { get; set; }

        public bool Homem { get; set; }
        public bool Mulher { get; set; }
        public bool Casal { get; set; }
        public bool Trans { get; set; }
    }
}
