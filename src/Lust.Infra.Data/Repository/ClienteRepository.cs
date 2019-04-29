using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lust.Domain.Clientes;
using Lust.Domain.Clientes.Interfaces;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

using System.Linq.Expressions;
using Lust.Domain.Query;

namespace Lust.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(LustContext context)
            : base(context)
        {

        }

        public override Task<Cliente> GetByIdAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return null;

            return DbSet
                .Include(q => q.Caracteristica).ThenInclude(q=>q.AtendeGeneros)
                .Include(q => q.Preferencia).ThenInclude(q => q.PrefereGeneros)
                .Include(q => q.Fotos)
                .Include(q => q.Videos)
                .Include(q => q.Assinaturas)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        public Cliente GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public Task<Cliente> GetByEmailAsync(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }        

        public Task<List<Cliente>> GetContatosAsync(Guid id)
        {
            return Db.Contatos.AsNoTracking()
                .Where(q => q.ClienteId == id)
                .Select(q => q.ContatoCliente)
                .ToListAsync();

            //return DbSet.AsNoTracking()
            //   .Where(q => q.Id == id)
            //   .SelectMany(q => q.Contatos.Select(c => c.ContatoCliente))
            //   .ToListAsync();
        }

        public void Add(Foto foto)
        {
            Db.Fotos.Add(foto);

        }
              

       
        

        public Task<IEnumerable<ClienteQuery>> GetClientesProximosAsync(Guid Id, int Skip, int Take, string Query="")
        {
            bool QueryVazia = String.IsNullOrEmpty(Query);
            Query = StringParaLike(Query);
            
            var sql = @"
                        select * from(
                        select 
	                        oferece.Id,
	                        oferece.Nome, 
	                        oferece.DataNascimento,
	                        FLOOR(DATEDIFF(DAY,oferece. DataNascimento, GETDATE()) / 365.25) as Idade,
	                        oferece.Genero, 	
	                        oferece.CurtaDescricao,
	                        Caracteristica.Valor30min,
	                        Caracteristica.Valor1Hora,
	                        Caracteristica.Valor2horas,
	                        Caracteristica.ValorPernoite,
	                        oferece.FotoDeCapa,
	                        oferece.FotoDePerfil,
	                        oferece.Nota,
	                        desfruta.Location.STDistance(oferece.Location) / 1000 as Distancia
                        from cliente as desfruta inner join
                        Preferencia on desfruta.Id = preferencia.id
                            cross join
                        cliente as oferece inner join
                        Caracteristica on oferece.id = Caracteristica.id
                        where desfruta.Id = @Id and oferece.EstaOferecendo=1
                        and (@QueryVazia = 1 or oferece.Nome like @Query)
                        and Caracteristica.Valor1Hora >= Preferencia.PrecoMinimo and Caracteristica.Valor1Hora <=Preferencia.PrecoMaximo
                        and FLOOR(DATEDIFF(DAY,oferece. DataNascimento, GETDATE()) / 365.25) >= Preferencia.IdadeMinima and FLOOR(DATEDIFF(DAY,oferece. DataNascimento, GETDATE()) / 365.25) <= Preferencia.IdadeMaxima
                        and desfruta.Location.STDistance(oferece.Location) / 1000 <= Preferencia.Distancia
                        and EXISTS(
		                        SELECT 1
		                        FROM PreferenciaGenero
		                        WHERE PreferenciaId = Preferencia.id and Genero = oferece.Genero
	                        )
                        ) as q
                        order by q.Distancia
                        OFFSET @Skip ROWS
                        FETCH NEXT @Take ROWS ONLY 

";

            return Db.Database.GetDbConnection().QueryAsync<ClienteQuery>(sql, new { Id,Query, Skip, Take, QueryVazia });
        }

        public Task AtualizarPosicaoAsync(Guid Id, float Latitude, float Longitude)
        {
            var sql = @"update Cliente set Latitude = @Latitude, Longitude = @Longitude where Id = @Id";
            return Db.Database.GetDbConnection().ExecuteAsync(sql, new {  Id,  Latitude, Longitude });
        }



        public Task<Preferencia> GetPreferenciaAsync(Guid Id)
        {
            return Db.Preferencias.Include(q => q.PrefereGeneros).SingleOrDefaultAsync(q => q.Id == Id);
        }

        public async Task<Tuple<List<Cliente>, int>> Listar(int Skip, int Take, string Query = "", string Sort = "nome", string Direction = "asc")
        {
            bool QueryVazia = String.IsNullOrEmpty(Query);
            Query = StringParaLike(Query);

            var sql = $@"select id, nome, email, cpf, estadesfrutando, estaoferecendo, genero
                        from cliente
                        where @QueryVazia = 1 or nome like @Query or email like @Query
                        order by {Sort} {Direction}
                        OFFSET @Skip ROWS
                        FETCH NEXT  @Take ROWS ONLY ;
                        select count(*)
                        from cliente 
                        where @QueryVazia = 1 or nome like @Query or email like @Query";
            
            using (var multi = await Db.Database.GetDbConnection().QueryMultipleAsync(sql, new { Query, Skip, Take, QueryVazia }))
            {
                var lista = multi.Read<Cliente>().ToList();
                var totalRows = multi.Read<int>().Single();
                var resp = new Tuple<List<Cliente>, int>(lista, totalRows);
                return resp;
            }

            


        }

        public async Task<PerfilQuery> GetPerfilAsync(Guid IdOferece, Guid IdDesfruta)
        {
            var sql = @"
                    select 
                    oferece.Id,
                    oferece.Nome, 	                        
                    FLOOR(DATEDIFF(DAY,oferece. DataNascimento, GETDATE()) / 365.25) as Idade,	                        
                    oferece.CurtaDescricao,
                    oferece.LongaDescricao,
                    Caracteristica.Valor30min,
                    Caracteristica.Valor1Hora,
                    Caracteristica.Valor2horas,
                    Caracteristica.ValorPernoite,
                    Caracteristica.LocalProprio, 
                    oferece.FotoDeCapa,
                    oferece.FotoDePerfil,
                    oferece.Nota,
                    oferece.NotaHigiene ,
                    oferece.NotaPrazer ,
                    oferece.NotaFidelidadeAsFotos ,
                    oferece.NotaEducacao ,
                    oferece.NotaAmbiente ,
                    oferece.NotaPontualidade ,
                    desfruta.Location.STDistance(oferece.Location) / 1000 as Distancia,
                    oferece.Latitude, 
                    oferece.Longitude
                    from cliente as desfruta 
                    cross join
                    cliente as oferece inner join
                    Caracteristica on oferece.id = Caracteristica.id
                    where oferece.id = @IdOferece and desfruta.id = @IdDesfruta;

                    select	Id, Descricao 
                    from foto
                    where ClienteId = @IdOferece and StatusAnalise = 1
                    order by ordem;
                    
                    select DoteId,descricao from DoteCaracteristica as dc inner join 
                    dote on dc.DoteId = dote.id
                    where caracteristicaid = @IdOferece
                    order by descricao
";

            using (var multi = await Db.Database.GetDbConnection().QueryMultipleAsync(sql, new { IdOferece, IdDesfruta }))
            {
                var perfil = multi.Read<PerfilQuery>().SingleOrDefault();
                if (perfil == null) return null;

                var fotos = multi.Read<FotoQuery>().ToList();
                perfil.Fotos = fotos;

                var dotes = multi.Read<DoteQuery>().ToList();
                perfil.Dotes = dotes;

                return perfil;
            }

        }

        //public async Task Update(Preferencia preferencia)
        //{

        //    //var p Db.Preferencias.Include(q => q.PrefereGeneros).SingleOrDefaultAsync(q => q.Id == preferencia.Id);
        //    Db.Preferencias.Update(preferencia);
        //}
    }
}
