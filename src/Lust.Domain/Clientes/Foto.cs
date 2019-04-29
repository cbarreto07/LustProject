
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{
    public class Foto : Entity
    {
        public string Descricao { get;  set; }

        public int Ordem { get; set; }
        
        public Guid ClienteId { get;  set; }

        public EnumStatusAnalise StatusAnalise { get; set; }

        public string MotivoReprovado { get; set; }
        
        // EF Propriedade de Navegação
        public virtual Cliente Cliente { get;  set; }

        public Foto(Guid id, string descricao, Guid clienteId, EnumStatusAnalise statusAnalise)
        {
            Id = id;
            Descricao = descricao;
            ClienteId = clienteId;
            StatusAnalise = statusAnalise;


        }

        // Construtor para o EF
        public Foto() { }

        
    }
}
