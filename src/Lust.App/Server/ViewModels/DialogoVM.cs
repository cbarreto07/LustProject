using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class DialogoVM
    {
        public DialogoVM()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid  Who { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}
