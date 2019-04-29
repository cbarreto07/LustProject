using System.Threading.Tasks;
using Lust.Domain.Core.Commands;
using Lust.Domain.Core.Events;

namespace Lust.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}