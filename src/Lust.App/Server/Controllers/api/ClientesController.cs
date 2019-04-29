using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lust.App.Server.Interfaces;
using Lust.App.Server.ViewModels;
using Lust.Domain.Core.Notifications;
using Lust.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lust.App.Server.Controllers.api
{
    [Produces("application/json")]
    [Route("api/clientes")]
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public class ClientesController : BaseController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(
            INotificationHandler<DomainNotification> notifications, 
            IUser user,
            IClienteAppService clienteAppService) : base(notifications, user)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]int skip = 0, [FromQuery]int take = 12, [FromQuery]string query = "", [FromQuery]string sort = "nome", [FromQuery]string direction = "asc")
        {
            var resp = await _clienteAppService.Listar(skip, take, query, sort, direction);
            return Response(resp.Item1, resp.Item2);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {            
            return Response(await _clienteAppService.ObterPorIdAsync(Id));
        }

    }
}