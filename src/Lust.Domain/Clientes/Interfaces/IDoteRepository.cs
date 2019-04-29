using Lust.Domain.Interfaces;
using Lust.Domain.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lust.Domain.Clientes.Interfaces
{
    public interface IDoteRepository : IRepository<Dote>
    {
        Task<Tuple<List<DoteQuery>, int>> ListarAsync(int skip, int take, string query, string sort, string direction);
    }
}
