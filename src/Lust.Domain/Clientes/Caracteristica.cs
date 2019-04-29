using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class Caracteristica
    {

        public Guid Id { get; set; }

        public EnumAparencia? Aparencia { get; set; }

        public int? Manequim { get; set; }

        public EnumOlhos? Olhos { get; set; }

        public int? Peso { get; set; }
        public int? Busto { get; set; }
        public int? Cintura { get; set; }
        public int? Pes { get; set; }
        public int? Quadril { get; set; }
        public decimal? Altura { get; set; }

        public virtual ICollection<DoteCaracteristica> Dotes { get; set; }

        public virtual ICollection<CaracteristicaGenero> AtendeGeneros { get; set; }

        public decimal Valor30min { get; set; }

        public decimal Valor1Hora { get; set; }

        public decimal Valor2horas { get; set; }

        public decimal ValorPernoite { get; set; }        

        public bool LocalProprio { get; set; }

        public Cliente Cliente { get; set; }

    }
}
