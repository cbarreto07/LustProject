using Lust.App.Server.ViewModels;
using Lust.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Interfaces
{
    public interface IDoteAppService
    {
        Task<Tuple<List<DoteListaVM>, int>> ListarAsync(int skip, int take, string query, string sort, string direction);
        Task<DoteVM> ObterPorIdAsync(Guid id);
        Task<DoteVM> Criar(DoteVM model);
        Task<DoteVM> Atualizar(DoteVM model);
        Task Excluir(Guid id);
    }
}
