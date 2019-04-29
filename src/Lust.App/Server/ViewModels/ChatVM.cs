using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.ViewModels
{
    public class ChatVM
    {
        public ChatVM()
        {
            Id = Guid.NewGuid();
            Dialogo = new List<DialogoVM>();
        }
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public ContatoViewModel Cliente { get; set; }
        public int Unread { get; set; }
        public DateTime LastMessageTime { get; set; }
        public List<DialogoVM> Dialogo { get; set; }
    }   
}
