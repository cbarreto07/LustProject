
using Lust.Domain.Interfaces;
using Lust.Domain.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lust.Domain.Clientes.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente GetByEmail(string email);
        Task<List<Cliente>> GetContatosAsync(Guid id);    

        void Add(Foto foto);

        Task<IEnumerable<ClienteQuery>> GetClientesProximosAsync(Guid guid, int skip, int take, string query="");
        Task AtualizarPosicaoAsync(Guid guid, float latitude, float longitude);
        Task<Preferencia> GetPreferenciaAsync(Guid Id);
        Task<Tuple<List<Cliente>, int>> Listar(int Skip,  int Take , string Query = "", string Sort = "nome", string Direction = "asc");
        //Task Update(Preferencia preferencia);
        Task<PerfilQuery> GetPerfilAsync(Guid IdOferece,Guid IdDesfruta );

    }
}