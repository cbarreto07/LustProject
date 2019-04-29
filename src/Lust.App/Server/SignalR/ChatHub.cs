using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Lust.Infra.CrossCutting.Identity.Models;
using Lust.Domain.Chats.Interfaces;
using Lust.Infra.Data.Context;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using Lust.Domain.Chats;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;

namespace Lust.App.Server.SignalR
{

    public class Chat : Hub
    {
        private readonly LustContext _context;
        private readonly IMapper _mapper;
        private readonly IClienteAppService _clienteAppService;
        
        public Chat(IMapper mapper, LustContext context, IClienteAppService clienteAppService)
        {
            _mapper = mapper;
            _context = context;
            _clienteAppService = clienteAppService;
        }

        //private IUser user,
        //public Chat(IUser user)
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }

        public async Task Position(PositionVM position)
        {
            if (!(position.Latitude == 0 && position.Longitude == 0))
                try
                {
                    await _clienteAppService.AtualizarPosicaoClienteLogadoAsync(position);
                }catch(Exception e)
                {
                    var a = 0;
                }

            await Clients.Caller.SendAsync("Position", "OK");
        }


        public Task Message(DataHudComunication message)
        {

            if (message.to != null && message.to != default(Guid))
            {
                message.from = Guid.Parse(this.Context.User.GetUserId());
                _context.Dialogos.Add(new Dialogo()
                {
                    Id = message.id,
                    ChatId = message.chatId,
                    ClienteId = message.from,
                    Mensagem = message.message
                });
                _context.SaveChanges();

                return Clients.User(message.to.ToString()).SendAsync("Message", message);

            }

            
            return Clients.All.SendAsync("Message", message);

        }

        //public Task Chats()
        //{
        //    Guid id = Guid.Parse(this.Context.User.GetUserId());

        //    var chats = _context.Chats.AsNoTracking()
        //       //.Include(q => q.Dialogos)
        //       .Where(q => q.Clientes.Any(c => c.ClienteId == id))
        //       .Select(q => new ChatVM()
        //       {
        //           Id = q.Id,
        //            //Nome = q.Clientes.FirstOrDefault(c => c.ClienteId != id).Cliente.Nome,
        //            Dialogos = q.Dialogos.OrderBy(d => d.DataHoraCriacao).Take(50).OrderByDescending(d => d.DataHoraCriacao).Select(d => new DialogoVM()
        //           {
        //               Id = d.Id,
        //               Mensagem = d.Mensagem,
        //               Who = d.ClienteId,
        //               Time = d.DataHoraCriacao
        //           }).ToList()

        //       })
        //       .ToList();
        //    return Clients.Caller.SendAsync("chats", chats);
        //}

        //public Task Contatos()
        //{
            
        //    Guid id = Guid.Parse(this.Context.User.GetUserId());
        //    var list = _context.Clientes
        //        .AsNoTracking()
        //        .Where(q=>q.Id != id)
        //        .Select(q => new ClienteVM()
        //        { 
        //             Id = q.Id
        //        }).ToList();


        //    return Clients.Caller.SendAsync("contatos", list);
        //}

        //public Task List()
        //{
        //    Guid id = Guid.Parse(this.Context.User.GetUserId());
        //    var list = _context.Chats.AsNoTracking()
        //       //.Include(q => q.Dialogos)
        //       .Where(q => q.Clientes.Any(c => c.ClienteId == id))
        //       .Select(q => new ChatListVM()
        //       {
        //           Id = q.Id,
        //           Contato = q.Clientes
        //               .Select(c => new ClienteVM()
        //               {
        //                   Id = c.ClienteId,
        //                   Nome = c.Cliente.Nome
        //               })
        //               .FirstOrDefault(c => c.Id != id)
        //       })
        //       .ToList();
                    
                    

                   


                   
        //    return Clients.Caller.SendAsync("list", list);
        //}

        //public override Task OnConnectedAsync()
        //{
        //    //   await Clients.All.SendAsync("Send", $"{Context.ConnectionId} joined");
        //    //await Clients.User.Send
        //    Guid id = Guid.Parse(this.Context.User.GetUserId());


        //    ChatDataVM vm = new ChatDataVM();

           

        //    vm.Chats = chats;

        //    var chatList = _context.Chats.AsNoTracking()


        //    //var chatVM = _mapper.Map<IEnumerable<ChatDataVM>>(chats);

        //    return Clients.Caller.SendAsync("Data", vm);
        //}
    }


    public class ChatVM
    {
        public string Nome { get; set; }
        public virtual ICollection<DialogoVM> Dialogos { get; set; }
        public Guid Id { get; set; }        
    }

    public class ChatListVM
    {
        public Guid Id { get; set; }
        //public Guid ClienteId { get; set; }
        //public string Nome { get; set; }
        public ClienteVM Contato { get; set; }
        public int? UnRead { get; set; }
        public DateTime LastMessageTime { get; set; }
    }


    public class ClienteVM
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Humor { get; set; }

    }

    public class DialogoVM
    {
        public Guid Id { get; set; }
        public Guid Who { get; set; }
        public string Mensagem { get; set; }
        public DateTime Time { get; set; }

    }

    public class DataHudComunication
    {
        public DataHudComunication()
        {
            time = DateTime.Now;
        }
        public Guid id { get; set; }
        public Guid chatId { get; set; }
        public Guid from { get; set; }
        public Guid to { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
    }
}