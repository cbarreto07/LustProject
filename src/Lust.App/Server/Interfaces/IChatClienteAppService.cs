
using Lust.App.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Interfaces
{
    public interface IChatClienteAppService : IDisposable
    {
        

        Task<ChatVM> ObterOuCriarPeloIdDoContatoAsync(Guid id);
        Task<List<ChatVM>> ObterTodosOsChatsAsync();
        




    }
}
