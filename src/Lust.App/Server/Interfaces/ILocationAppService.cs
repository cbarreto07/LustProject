using Lust.App.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Interfaces
{
    public interface ILocationAppService
    {

        Task<Location> GetLocation(string cep);
    }
}
