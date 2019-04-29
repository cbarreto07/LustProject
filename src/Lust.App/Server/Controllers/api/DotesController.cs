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
    [Route("api/dotes")]
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public class DotesController : BaseController
    {
        private readonly IDoteAppService _doteAppService;

        public DotesController(
            INotificationHandler<DomainNotification> notifications, 
            IUser user,
            IDoteAppService doteAppService) : base(notifications, user)
        {
            _doteAppService = doteAppService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery]int skip = 0, [FromQuery]int take = 12, [FromQuery]string query = "", [FromQuery]string sort = "descricao", [FromQuery]string direction = "asc")
        {
            var resp = await _doteAppService.ListarAsync(skip, take, query, sort, direction);
            return Response(resp.Item1, resp.Item2);
        }

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> Get(Guid Id)
        {            
            return Response(await _doteAppService.ObterPorIdAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DoteVM model)
        {
            return Response(await _doteAppService.Criar(model));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]DoteVM model)
        {
            return Response(await _doteAppService.Atualizar(model));
        }

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _doteAppService.Excluir(Id);
            return Response();
        }

    }
}