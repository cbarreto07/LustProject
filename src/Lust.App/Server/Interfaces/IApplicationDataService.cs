using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lust.App.Server.Interfaces
{
    public interface IApplicationDataService
    {
        Task<object> GetApplicationData(HttpContext context);
    }
}