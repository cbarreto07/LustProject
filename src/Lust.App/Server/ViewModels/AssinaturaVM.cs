using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class AssinaturaVM
    {
        public Guid ClienteId { get; set; }

        public Guid PlanoId { get; set; }

        public decimal Valor { get; set; }
    }
}
