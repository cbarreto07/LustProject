using Lust.Domain.Clientes;
using Lust.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Chats
{
    public class Dialogo : Entity
    {
        public Guid ClienteId { get; set; }
        public Guid ChatId { get; set; }
        

        public string Mensagem { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Chat Chat { get; set; }


        public virtual ICollection<DialogoLeitura> DialogoLeituras { get; set; }
    }
}
