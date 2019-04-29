using Lust.Domain.Clientes;
using Lust.Domain.Clientes.Interfaces;
using Lust.Domain.Query;
using Lust.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Lust.Domain.Planos;
using Lust.Domain.Planos.Interfaces;

namespace Lust.Infra.Data.Repository
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {

        public PlanoRepository(LustContext context)
            : base(context)
        {

        }

        public async Task<Tuple<List<PlanoQuery>, int>> ListarAsync(int Skip, int Take, string Query = "", string Sort = "descricao", string Direction = "asc", EnumDestinado? Destinado=null)
        {
            bool QueryVazia = String.IsNullOrEmpty(Query);
            Query = StringParaLike(Query);

            var sql = $@"select Id, Titulo, Destinado,Descricao, Ordem, QuantidadeMeses, Valor
                        from plano
                        where (@QueryVazia = 1 or Titulo like @Query)
                        and (@Destinado is null or Destinado = @Destinado)
                        order by {Sort} {Direction}
                        OFFSET @Skip ROWS
                        FETCH NEXT  @Take ROWS ONLY ;
                        select count(*)
                        from plano 
                        where (@QueryVazia = 1 or Titulo like @Query)
                        and (@Destinado is null or Destinado = @Destinado) ";
            using (var multi = await Db.Database.GetDbConnection().QueryMultipleAsync(sql, new { Query, Skip, Take, QueryVazia, Destinado }))
            {
                var lista = multi.Read<PlanoQuery>().ToList();
                var totalRows = multi.Read<int>().Single();
                var resp = new Tuple<List<PlanoQuery>, int>(lista, totalRows);
                return resp;
            }
        }
    }
}
