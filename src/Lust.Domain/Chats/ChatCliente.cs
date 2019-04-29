using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Chats
{
    public class ChatCliente
    {
        public Guid ClienteId { get; set; }
        public Guid ChatId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
