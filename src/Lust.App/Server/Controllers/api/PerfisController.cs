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
    [Route("api/perfis")]
    public class PerfisController : BaseController
    {
        private readonly IClienteAppService _clienteAppService;

        public PerfisController(
            INotificationHandler<DomainNotification> notifications, 
            IUser user,
            IClienteAppService clienteAppService) : base(notifications, user)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]int skip = 0, [FromQuery]int take = 12, [FromQuery]string query = "")
        {
            var clientes = await _clienteAppService.ClientesProximosClienteLogadoAsync(skip, take, query);
            return Response(clientes);
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            return Response(await _clienteAppService.ObterPerfilAsync(Id));
        }

    }
}