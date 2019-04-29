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

namespace Lust.Infra.Data.Repository
{
    public class DoteRepository : Repository<Dote>, IDoteRepository
    {

        public DoteRepository(LustContext context)
            : base(context)
        {

        }

        public async Task<Tuple<List<DoteQuery>, int>> ListarAsync(int Skip, int Take, string Query = "", string Sort = "descricao", string Direction = "asc")
        {
            bool QueryVazia = String.IsNullOrEmpty(Query);
            Query = StringParaLike(Query);

            var sql = $@"select id, descricao
                        from dote
                        where @QueryVazia = 1 or descricao like @Query 
                        order by {Sort} {Direction}
                        OFFSET @Skip ROWS
                        FETCH NEXT  @Take ROWS ONLY ;
                        select count(*)
                        from dote 
                        where @QueryVazia = 1 or descricao like @Query ";
            using (var multi = await Db.Database.GetDbConnection().QueryMultipleAsync(sql, new { Query, Skip, Take, QueryVazia }))
            {
                var lista = multi.Read<DoteQuery>().ToList();
                var totalRows = multi.Read<int>().Single();
                var resp = new Tuple<List<DoteQuery>, int>(lista, totalRows);
                return resp;
            }
        }
    }
}
