using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Lust.Domain.Assinaturas;
using Lust.Domain.Chats;
using Lust.Domain.Clientes;
using Lust.Domain.Compras;
using Lust.Domain.Core.Models;
using Lust.Domain.Localises;
using Lust.Domain.Planos;
using Lust.Domain.Sociais;
using Lust.Infra.Data.Mappings;
using Lust.Infra.Data.Mappings.Assinaturas;
using Lust.Infra.Data.Mappings.Chats;
using Lust.Infra.Data.Mappings.Clientes;
using Lust.Infra.Data.Mappings.Compras;
using Lust.Infra.Data.Mappings.Localises;
using Lust.Infra.Data.Mappings.Planos;
using Lust.Infra.Data.Mappings.Sociais;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lust.Infra.Data.Context
{
    public class LustContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Preferencia> Preferencias { get; set; }
        public DbSet<Caracteristica> Caracteristicas { get; set; }
        public DbSet<Dote> Dotes { get; set; }
        public DbSet<DoteCaracteristica> DotesCaracteristicas { get; set; }
        public DbSet<CaracteristicaGenero> CaracteristicaGeneros { get; set; }
        public DbSet<PreferenciaGenero> PreferenciaGeneros { get; set; }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }

        public DbSet<Plano> Planos { get; set; }

        public DbSet<Avaliacao> Avaliacoes { get; set; }

        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }


        public DbSet<Chat> Chats { get; set; }
        public DbSet<Dialogo> Dialogos { get; set; }
        public DbSet<DialogoLeitura> DialogoLeituras { get; set; }
        public DbSet<ChatCliente> ChatsClientes { get; set; }

        public LustContext(DbContextOptions<LustContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FotoMap());
            modelBuilder.ApplyConfiguration(new VideoMap());
            modelBuilder.ApplyConfiguration(new PlanoMap());
            modelBuilder.ApplyConfiguration(new AssinaturaMap());
            modelBuilder.ApplyConfiguration(new PagamentoMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoMap());
            modelBuilder.ApplyConfiguration(new CultureMap());
            modelBuilder.ApplyConfiguration(new ResourceMap());
            modelBuilder.ApplyConfiguration(new ContactUsMap());

            modelBuilder.ApplyConfiguration(new PreferenciaMap());
            modelBuilder.ApplyConfiguration(new CaracteristicaMap());
            modelBuilder.ApplyConfiguration(new DoteMap());
            modelBuilder.ApplyConfiguration(new DoteCaracteristicaMap());                                    
            modelBuilder.ApplyConfiguration(new CaracteristicaGeneroMap());
            modelBuilder.ApplyConfiguration(new PreferenciaGeneroMap());



            modelBuilder.ApplyConfiguration(new ChatMap());
            modelBuilder.ApplyConfiguration(new DialogoMap());
            modelBuilder.ApplyConfiguration(new DialogoLeituraMap());
            modelBuilder.ApplyConfiguration(new ChatClienteMap());
            modelBuilder.ApplyConfiguration(new ContatoMap());
            modelBuilder.ApplyConfiguration(new CompraMap());
            

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // get the configuration from the app settings
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
            
        //    // define the database to use
        //    optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        //}


        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = (Entity)changedEntity.Entity;

                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            if (entity.Id == null || entity.Id == Guid.Empty)
                                entity.Id = Guid.NewGuid();
                            entity.DataHoraCriacao = DateTime.Now;                            
                            break;

                        case EntityState.Modified:
                            entity.DataHoraAlteracao = DateTime.Now;
                            break;

                    }
                }
            }

            return base.SaveChanges();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = (Entity)changedEntity.Entity;

                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            if (entity.Id == null || entity.Id == Guid.Empty)
                                entity.Id = Guid.NewGuid();
                            entity.DataHoraCriacao = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            entity.DataHoraAlteracao = DateTime.Now;
                            break;

                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        
    }
}
