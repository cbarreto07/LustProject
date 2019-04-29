using Lust.Domain.Clientes;
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Chats
{
    public class DialogoLeitura : Entity
    {
        public Guid DialogoId { get; set; }
        public Guid ClienteId { get; set; }

        

        public Dialogo Dialogo { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
