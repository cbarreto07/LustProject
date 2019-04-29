using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class FotoVM
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public EnumStatusAnalise StatusAnalise { get; set; }
        public string MotivoReprovado { get; set; }
    }
}
