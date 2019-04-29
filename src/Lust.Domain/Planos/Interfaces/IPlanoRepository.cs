using Lust.Domain.Interfaces;
using Lust.Domain.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lust.Domain.Planos.Interfaces
{
    public interface IPlanoRepository : IRepository<Plano>
    {
        Task<Tuple<List<PlanoQuery>, int>> ListarAsync(int skip, int take, string query, string sort, string direction, EnumDestinado? Destinado);
    }
}