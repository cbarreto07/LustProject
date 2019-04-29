using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Assinaturas
{
    public class Pagamento : Entity
    {
                
        public Guid AssinaturaId { get;  set; }

        // EF Propriedade de Navegação
        public virtual Assinatura Assinatura { get;  set; }

        public DateTime Validade { get; set; }


        // Construtor para o EF
        public Pagamento() { }

        
    }
}
