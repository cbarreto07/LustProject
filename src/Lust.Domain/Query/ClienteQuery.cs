using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Query
{
    public class ClienteQuery
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }

        public EnumGenero Genero { get; set; }

        public decimal ValorCobrado { get; set; }

        public string CurtaDescricao { get; set; }

        public Decimal Valor30min { get; set; }

        public Decimal Valor1Hora { get; set; }

        public Decimal Valor2horas { get; set; }

        public Decimal ValorPernoite { get; set; }

        public Guid? FotoDeCapa { get; set; }

        public Guid? FotoDePerfil { get; set; }

        public Decimal Nota { get; set; }

        public decimal Distancia { get; set; }
    }
}
