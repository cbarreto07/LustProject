using Lust.Domain.Clientes;
using Lust.Domain.Core.Models;
using Lust.Domain.Planos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Assinaturas
{
    public class Assinatura : Entity
    {
       
        
        public Guid ClienteId { get;  set; }

        public Guid PlanoId { get; set; }

        public decimal Valor { get; set; }
        
        // EF Propriedade de Navegação
        public virtual Cliente Cliente { get;  set; }

        public virtual Plano Plano { get; set; }

        public bool Ativo { get; set; }

        public string PreApprovalCode { get; set; }

        public virtual ICollection<Pagamento> Pagamentos { get; set; }

        

        // Construtor para o EF
        public Assinatura() { }

        
    }
}
