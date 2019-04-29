using Lust.Domain.Clientes;
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Chats
{
    public class Chat : Entity
    {
        public string Nome { get; set; }
        public virtual ICollection<ChatCliente> Clientes { get; set; }
        public virtual ICollection<Dialogo> Dialogos { get; set; }
    }
}
