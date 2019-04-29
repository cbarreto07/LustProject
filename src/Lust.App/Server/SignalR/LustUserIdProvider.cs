using AspNetCoreSpa.Server.Extensions;
using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lust.App.Server.SignalR
{
    public class LustUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.GetUserId();//.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}