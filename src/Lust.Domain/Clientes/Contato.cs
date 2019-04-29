using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Domain.Clientes
{ 
    public class Contato
    {
        public Guid ClienteId { get; set; }
        public Guid ContatoClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Cliente ContatoCliente { get; set; }
    }
}
