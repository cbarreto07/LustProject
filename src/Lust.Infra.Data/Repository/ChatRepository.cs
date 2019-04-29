using Lust.Domain.Chats;
using Lust.Domain.Chats.Interfaces;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lust.Infra.Data.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(LustContext context) : base(context)
        {
        }

        public Task<Chat> GetChatAsync(Guid clienteId1, Guid clienteId2)
        {
            return DbSet.AsNoTracking()
                .Include(q => q.Dialogos)
                 .Include(q => q.Clientes)
                 .FirstOrDefaultAsync(q => q.Clientes.Any(c => c.ClienteId == clienteId1) && q.Clientes.Any(c => c.ClienteId == clienteId2));
                 
        }

        public  Task<List<Chat>> GetChatsComDialogosPeloClienteIdAsync(Guid id)
        {
            return DbSet.AsNoTracking()
                 .Include(q => q.Dialogos)
                 .Include(q => q.Clientes) 
                 .ThenInclude(q=>q.Cliente)
                 .Where(q => q.Clientes.Any(c => c.ClienteId == id))
             .ToListAsync();
                
            
        }
    }
}
