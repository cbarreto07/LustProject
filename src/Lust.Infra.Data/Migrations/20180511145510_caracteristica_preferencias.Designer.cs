﻿// <auto-generated />
using System;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lust.Infra.Data.Migrations
{
    [DbContext(typeof(LustContext))]
    [Migration("20180511145510_caracteristica_preferencias")]
    partial class caracteristica_preferencias
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lust.Domain.Assinaturas.Assinatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<Guid>("PlanoId");

                    b.Property<string>("PreApprovalCode")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PlanoId");

                    b.ToTable("Assinatura");
                });

            modelBuilder.Entity("Lust.Domain.Assinaturas.Pagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssinaturaId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("AssinaturaId");

                    b.ToTable("Pagamento");
                });

            modelBuilder.Entity("Lust.Domain.Chats.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Lust.Domain.Chats.ChatCliente", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("ChatId");

                    b.HasKey("ClienteId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatCliente");
                });

            modelBuilder.Entity("Lust.Domain.Chats.Dialogo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChatId");

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Mensagem")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Dialogo");
                });

            modelBuilder.Entity("Lust.Domain.Chats.DialogoLeitura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<Guid>("DialogoId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("DialogoId");

                    b.ToTable("DialogoLeitura");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Caracteristica", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<bool>("LocalProprio");

                    b.Property<decimal>("Valor1Hora");

                    b.Property<decimal>("Valor2horas");

                    b.Property<decimal>("Valor30min");

                    b.Property<decimal>("ValorPernoite");

                    b.HasKey("Id");

                    b.ToTable("Caracteristica");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.CaracteristicaGenero", b =>
                {
                    b.Property<Guid>("CaracteristicaId");

                    b.Property<int>("Genero");

                    b.HasKey("CaracteristicaId", "Genero");

                    b.ToTable("CaracteristicaGenero");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(11);

                    b.Property<string>("CurtaDescricao");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("EstaDesfrutando");

                    b.Property<bool>("EstaOferecendo");

                    b.Property<Guid?>("FotoDeCapa")
                        .HasColumnName("FotoDeCapa");

                    b.Property<Guid?>("FotoDePerfil");

                    b.Property<int>("Genero");

                    b.Property<double>("Latitude");

                    b.Property<string>("LongaDescricao");

                    b.Property<double>("Longitude");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<decimal>("Nota");

                    b.Property<decimal>("NotaAmbiente");

                    b.Property<decimal>("NotaEducacao");

                    b.Property<decimal>("NotaFidelidadeAsFotos");

                    b.Property<decimal>("NotaHigiene");

                    b.Property<decimal>("NotaPontualidade");

                    b.Property<decimal>("NotaPrazer");

                    b.Property<decimal>("ValorCobrado");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Contato", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("ContatoClienteId");

                    b.HasKey("ClienteId", "ContatoClienteId");

                    b.HasIndex("ContatoClienteId");

                    b.ToTable("Contato");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Dote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Dote");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.DoteCaracteristica", b =>
                {
                    b.Property<Guid>("CaracteristicaId");

                    b.Property<Guid>("DoteId");

                    b.HasKey("CaracteristicaId", "DoteId");

                    b.HasIndex("DoteId");

                    b.ToTable("DoteCaracteristica");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Foto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("MotivoReprovado")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Ordem");

                    b.Property<int>("StatusAnalise");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Foto");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Preferencia", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<decimal>("Distancia");

                    b.Property<int>("IdadeMaxima");

                    b.Property<int>("IdadeMinima");

                    b.Property<decimal>("PrecoMaximo");

                    b.Property<decimal>("PrecoMinimo");

                    b.HasKey("Id");

                    b.ToTable("Preferencia");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.PreferenciaGenero", b =>
                {
                    b.Property<Guid>("PreferenciaId");

                    b.Property<int>("Genero");

                    b.HasKey("PreferenciaId", "Genero");

                    b.ToTable("PreferenciaGenero");
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("MotivoReprovado")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Ordem");

                    b.Property<int>("StatusAnalise");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("Lust.Domain.Localises.Culture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Culture");
                });

            modelBuilder.Entity("Lust.Domain.Localises.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CultureId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Key")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Value")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("Lust.Domain.Planos.Plano", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ComentarEncontro");

                    b.Property<bool>("ComentarFotosOuVideos");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("Destaque");

                    b.Property<int>("Destinado");

                    b.Property<bool>("EntrarEmContato");

                    b.Property<bool>("ExibirAvaliacoe");

                    b.Property<int>("Fotos");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<decimal>("Valor");

                    b.Property<int>("Videos");

                    b.Property<bool>("VisualizarAvaliacoes");

                    b.HasKey("Id");

                    b.ToTable("Plano");
                });

            modelBuilder.Entity("Lust.Domain.Sociais.Avaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteAvaliadoId");

                    b.Property<Guid>("ClienteAvaliadorId");

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<decimal>("Nota");

                    b.HasKey("Id");

                    b.HasIndex("ClienteAvaliadoId");

                    b.HasIndex("ClienteAvaliadorId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("Lust.Domain.Sociais.ContactUs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataHoraAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("varchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("Lust.Domain.Assinaturas.Assinatura", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Assinaturas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Planos.Plano", "Plano")
                        .WithMany("Assinaturas")
                        .HasForeignKey("PlanoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Assinaturas.Pagamento", b =>
                {
                    b.HasOne("Lust.Domain.Assinaturas.Assinatura", "Assinatura")
                        .WithMany("Pagamentos")
                        .HasForeignKey("AssinaturaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Chats.ChatCliente", b =>
                {
                    b.HasOne("Lust.Domain.Chats.Chat", "Chat")
                        .WithMany("Clientes")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Chats")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Chats.Dialogo", b =>
                {
                    b.HasOne("Lust.Domain.Chats.Chat", "Chat")
                        .WithMany("Dialogos")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Dialogos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Chats.DialogoLeitura", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("DialogoLeituras")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Chats.Dialogo", "Dialogo")
                        .WithMany("DialogoLeituras")
                        .HasForeignKey("DialogoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Caracteristica", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithOne("Caracteristica")
                        .HasForeignKey("Lust.Domain.Clientes.Caracteristica", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.CaracteristicaGenero", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Caracteristica", "Caracteristica")
                        .WithMany("AtendeGeneros")
                        .HasForeignKey("CaracteristicaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Contato", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Contatos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Clientes.Cliente", "ContatoCliente")
                        .WithMany("ContatoDeOutros")
                        .HasForeignKey("ContatoClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.DoteCaracteristica", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Caracteristica", "Caracteristica")
                        .WithMany("Dotes")
                        .HasForeignKey("CaracteristicaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Clientes.Dote", "Dote")
                        .WithMany("Caracteristicas")
                        .HasForeignKey("DoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Foto", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Fotos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Preferencia", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithOne("Preferencia")
                        .HasForeignKey("Lust.Domain.Clientes.Preferencia", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.PreferenciaGenero", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Preferencia", "Preferencia")
                        .WithMany("PrefereGeneros")
                        .HasForeignKey("PreferenciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Clientes.Video", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "Cliente")
                        .WithMany("Videos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Localises.Resource", b =>
                {
                    b.HasOne("Lust.Domain.Localises.Culture", "Culture")
                        .WithMany("Resources")
                        .HasForeignKey("CultureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lust.Domain.Sociais.Avaliacao", b =>
                {
                    b.HasOne("Lust.Domain.Clientes.Cliente", "ClienteAvaliado")
                        .WithMany("AvaliacoesRecebidas")
                        .HasForeignKey("ClienteAvaliadoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lust.Domain.Clientes.Cliente", "ClienteAvaliador")
                        .WithMany("AvaliacoesFeitas")
                        .HasForeignKey("ClienteAvaliadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
