
using Lust.App.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lust.App.Server.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        
        Task RegistrarAsync(ClienteViewModel clienteViewModel);
        Task RegistrarAsync(Guid Id, String nome, string email, string celular, string cpf, DateTime dataNascimento, string cep);
                   
        Task<ClienteViewModel> ObterPorIdAsync(Guid id);
        
        Task Atualizar(ClienteViewModel clienteViewModel);
        Task Excluir(Guid id);

        Task<List<ContatoViewModel>> ContatosDoClienteLogadoAsync();

        Task AddFotoDoClienteLogadoAsync(Guid Id, string Descricao, Stream stream);
        Task AddFotoAsync(Guid clienteId, Guid Id, string Descricao, Stream stream);

        Task<List<ClienteBuscaViewModel>>  ClientesProximosClienteLogadoAsync(int Skip = 0,int Take = 12, string query = "");
        Task AtualizarPosicaoClienteLogadoAsync(PositionVM position);

        Task<PreferenciaVM> ObterPreferenciasAsync(Guid Id);
        Task AtualizaPreferenciasAsync(PreferenciaVM model);

        Task<Tuple<List<ClienteViewModel>, int>> Listar(int Skip = 0, int Take = 12, string Query = "", string Sort = "nome", string Direction = "asc");
        Task<PerfilVM> ObterPerfilAsync(Guid id);
    }
}
