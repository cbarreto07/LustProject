using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class ContatoViewModel
    {

        public ContatoViewModel()
        {
            Id = Guid.NewGuid();
            
    }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Humor { get; set; }
    }
}
