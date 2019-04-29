using AutoMapper;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;
using Lust.Domain.Chats;
using Lust.Domain.Chats.Interfaces;
using Lust.Domain.Clientes.Interfaces;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Services
{
    public class ChatClienteAppService : IChatClienteAppService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly INotificationHandler<DomainNotification> _notifications;
        private readonly IUser _user;

        public ChatClienteAppService(IMapper mapper, IChatRepository chatRepository, IClienteRepository clienteRepository, INotificationHandler<DomainNotification> notifications, IUser user)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _clienteRepository = clienteRepository;
            _notifications = notifications;
            _user = user;

        }

        public void Dispose()
        {
            _chatRepository.Dispose();
        }

        public async Task<ChatVM> ObterOuCriarPeloIdDoContatoAsync(Guid id)
        {
            var userId = _user.GetUserId();
            var contato =await _clienteRepository.GetByIdAsync(id);
            var chat =await  _chatRepository.GetChatAsync(id, userId);
            if(chat == null)
            {
                
                var chatId = Guid.NewGuid();
                chat = new Chat() {
                    Id = chatId,
                     Nome = _user.GetUserId().ToString() + " x " + id.ToString(),
                      Dialogos = new List<Dialogo>(),
                       Clientes = new List<ChatCliente>()
                       {
                           new ChatCliente(){ ChatId=chatId, ClienteId = id},
                           new ChatCliente(){ChatId=chatId, ClienteId = userId}
                       },
                };
                _chatRepository.Add(chat);
                await _chatRepository.SaveChangesAsync();
               

            }
            

            ChatVM model = new ChatVM()
            {
                Id = chat.Id,
                Cliente = _mapper.Map<ContatoViewModel>(contato),
                ClienteId = id,
                Dialogo = _mapper.Map<List<DialogoVM>>(chat.Dialogos) ,
                LastMessageTime =chat.Dialogos.OrderBy(q=>q.DataHoraCriacao).LastOrDefault()?.DataHoraCriacao ?? DateTime.Now,
                Unread = 0
            };

            return model;
        }

        public async Task<List<ChatVM>> ObterTodosOsChatsAsync()
        {
            var chats = await _chatRepository.GetChatsComDialogosPeloClienteIdAsync(_user.GetUserId());
            var userId = _user.GetUserId();

            var lista = chats.Select(chat => new ChatVM()
            {
                Id = chat.Id,
                Cliente = _mapper.Map<ContatoViewModel>(chat.Clientes.FirstOrDefault(q=>q.ClienteId!= userId)?.Cliente),                
                Dialogo = _mapper.Map<List<DialogoVM>>(chat.Dialogos),
                LastMessageTime = chat.Dialogos.LastOrDefault()?.DataHoraCriacao ?? DateTime.Now,
                Unread = 0//TODO

            }).ToList();


            return lista;
        }
    }
}
