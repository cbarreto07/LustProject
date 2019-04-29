using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class CaracteristicaVM
    {
        //public virtual ICollection<DoteCaracteristicaVM> Dotes { get; set; }

        //public virtual ICollection<CaracteristicaGenero> AtendeGeneros { get; set; }

        public decimal Valor30min { get; set; }

        public decimal Valor1Hora { get; set; }

        public decimal Valor2horas { get; set; }

        public decimal ValorPernoite { get; set; }

        public bool LocalProprio { get; set; }

        public bool Homem { get; set; }
        public bool Mulher { get; set; }
        public bool Casal { get; set; }
        public bool Trans { get; set; }

    }
}
