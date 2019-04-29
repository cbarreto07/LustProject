using Lust.Domain.Assinaturas;
using Lust.Domain.Clientes;
using Lust.Domain.Compras;
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Planos
{
    public class Plano : Entity
    {
        
        

        public string Titulo { get; set; }

        public string SubTitulo { get; set; }

        public string Descricao { get; set; }   
        
        public int Ordem { get; set; }

        public EnumDestinado Destinado { get; set; }

        public int QuantidadeMeses { get; set; }

        public decimal Valor { get; set; }

        public virtual ICollection<Assinatura> Assinaturas { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }



        // Construtor para o EF
        public Plano() { }

        
    }
}
