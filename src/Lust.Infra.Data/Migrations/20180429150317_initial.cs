using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lust.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AtendeCasal = table.Column<bool>(nullable: false),
                    AtendeHomem = table.Column<bool>(nullable: false),
                    AtendeMulher = table.Column<bool>(nullable: false),
                    AtendeTrans = table.Column<bool>(nullable: false),
                    Celular = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Cep = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    CurtaDescricao = table.Column<string>(nullable: true),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(nullable: true),
                    EstaDesfrutando = table.Column<bool>(nullable: false),
                    EstaOferecendo = table.Column<bool>(nullable: false),
                    FotoDeCapa = table.Column<Guid>(nullable: true),
                    Genero = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    LongaDescricao = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Nota = table.Column<decimal>(nullable: false),
                    PodeExibirEndereco = table.Column<bool>(nullable: false),
                    Valor1Hora = table.Column<decimal>(nullable: false),
                    Valor2horas = table.Column<decimal>(nullable: false),
                    Valor30min = table.Column<decimal>(nullable: false),
                    ValorCobrado = table.Column<decimal>(nullable: false),
                    ValorPernoite = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ComentarEncontro = table.Column<bool>(nullable: false),
                    ComentarFotosOuVideos = table.Column<bool>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Destaque = table.Column<bool>(nullable: false),
                    Destinado = table.Column<int>(nullable: false),
                    EntrarEmContato = table.Column<bool>(nullable: false),
                    ExibirAvaliacoe = table.Column<bool>(nullable: false),
                    Fotos = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Videos = table.Column<int>(nullable: false),
                    VisualizarAvaliacoes = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteAvaliadoId = table.Column<Guid>(nullable: false),
                    ClienteAvaliadorId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Nota = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Cliente_ClienteAvaliadoId",
                        column: x => x.ClienteAvaliadoId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Cliente_ClienteAvaliadorId",
                        column: x => x.ClienteAvaliadorId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatCliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(nullable: false),
                    ChatId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatCliente", x => new { x.ClienteId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_ChatCliente_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatCliente_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(nullable: false),
                    ContatoClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => new { x.ClienteId, x.ContatoClienteId });
                    table.ForeignKey(
                        name: "FK_Contato_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contato_Cliente_ContatoClienteId",
                        column: x => x.ContatoClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dialogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChatId = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Mensagem = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dialogo_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dialogo_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MotivoReprovado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Ordem = table.Column<int>(nullable: false),
                    StatusAnalise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MotivoReprovado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Ordem = table.Column<int>(nullable: false),
                    StatusAnalise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CultureId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Key = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Value = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resource_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    PlanoId = table.Column<Guid>(nullable: false),
                    PreApprovalCode = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinatura_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assinatura_Plano_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogoLeitura",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DialogoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogoLeitura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogoLeitura_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogoLeitura_Dialogo_DialogoId",
                        column: x => x.DialogoId,
                        principalTable: "Dialogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssinaturaId = table.Column<Guid>(nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_Assinatura_AssinaturaId",
                        column: x => x.AssinaturaId,
                        principalTable: "Assinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_ClienteId",
                table: "Assinatura",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_PlanoId",
                table: "Assinatura",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ClienteAvaliadoId",
                table: "Avaliacao",
                column: "ClienteAvaliadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_ClienteAvaliadorId",
                table: "Avaliacao",
                column: "ClienteAvaliadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatCliente_ChatId",
                table: "ChatCliente",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_ContatoClienteId",
                table: "Contato",
                column: "ContatoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogo_ChatId",
                table: "Dialogo",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogo_ClienteId",
                table: "Dialogo",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogoLeitura_ClienteId",
                table: "DialogoLeitura",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogoLeitura_DialogoId",
                table: "DialogoLeitura",
                column: "DialogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_ClienteId",
                table: "Foto",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_AssinaturaId",
                table: "Pagamento",
                column: "AssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_CultureId",
                table: "Resource",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_ClienteId",
                table: "Video",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "ChatCliente");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "DialogoLeitura");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Resource");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Dialogo");

            migrationBuilder.DropTable(
                name: "Assinatura");

            migrationBuilder.DropTable(
                name: "Culture");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Plano");
        }
    }
}
