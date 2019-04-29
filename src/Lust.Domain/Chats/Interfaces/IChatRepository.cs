using Lust.Domain.Chats;
using Lust.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lust.Domain.Chats.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<List<Chat>> GetChatsComDialogosPeloClienteIdAsync(Guid id);
        Task<Chat> GetChatAsync(Guid clienteId1, Guid clienteId2);

    }
}