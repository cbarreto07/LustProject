using Lust.Domain.Clientes;
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Sociais
{
    public class Avaliacao : Entity
    {
        public string Descricao { get;  set; }

        public decimal Nota { get; set; }        
        
        public Guid ClienteAvaliadorId { get;  set; }

        public Guid ClienteAvaliadoId { get; set; }

        // EF Propriedade de Navegação
        public virtual Cliente ClienteAvaliador { get;  set; }

        public virtual Cliente ClienteAvaliado { get; set; }

        
        // Construtor para o EF
        public Avaliacao() { }

        
    }
}
