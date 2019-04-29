using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lust.App.Server.Controllers.api
{
    [Produces("application/json")]
    [Route("api/ChatDoCliente")]
    public class ChatDoClienteController : BaseController
    {
        private readonly IChatClienteAppService _chatClienteAppService;

        public ChatDoClienteController(
            INotificationHandler<DomainNotification> notifications, 
            IUser user,
            IChatClienteAppService chatClienteAppService) : base(notifications, user)
        {
            _chatClienteAppService = chatClienteAppService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var chats = await _chatClienteAppService.ObterTodosOsChatsAsync();
            return Response(chats);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var chat = await _chatClienteAppService.ObterOuCriarPeloIdDoContatoAsync(Id);
            return Response(chat);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ChatVM model)
        {
            var chat = await _chatClienteAppService.ObterOuCriarPeloIdDoContatoAsync(model.ClienteId);
            return Response(chat);
        }
    }
}