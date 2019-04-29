using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lust.Infra.Data.Migrations
{
    public partial class caracteristica_preferencias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtendeCasal",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "AtendeHomem",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "AtendeMulher",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "AtendeTrans",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "PodeExibirEndereco",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "ValorPernoite",
                table: "Cliente",
                newName: "NotaPrazer");

            migrationBuilder.RenameColumn(
                name: "Valor30min",
                table: "Cliente",
                newName: "NotaPontualidade");

            migrationBuilder.RenameColumn(
                name: "Valor2horas",
                table: "Cliente",
                newName: "NotaHigiene");

            migrationBuilder.RenameColumn(
                name: "Valor1Hora",
                table: "Cliente",
                newName: "NotaFidelidadeAsFotos");

            migrationBuilder.AddColumn<decimal>(
                name: "NotaAmbiente",
                table: "Cliente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NotaEducacao",
                table: "Cliente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Caracteristica",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor30min = table.Column<decimal>(nullable: false),
                    Valor1Hora = table.Column<decimal>(nullable: false),
                    Valor2horas = table.Column<decimal>(nullable: false),
                    ValorPernoite = table.Column<decimal>(nullable: false),
                    LocalProprio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caracteristica_Cliente_Id",
                        column: x => x.Id,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraAlteracao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Distancia = table.Column<decimal>(nullable: false),
                    IdadeMinima = table.Column<int>(nullable: false),
                    IdadeMaxima = table.Column<int>(nullable: false),
                    PrecoMinimo = table.Column<decimal>(nullable: false),
                    PrecoMaximo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferencia_Cliente_Id",
                        column: x => x.Id,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaracteristicaGenero",
                columns: table => new
                {
                    CaracteristicaId = table.Column<Guid>(nullable: false),
                    Genero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicaGenero", x => new { x.CaracteristicaId, x.Genero });
                    table.ForeignKey(
                        name: "FK_CaracteristicaGenero_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoteCaracteristica",
                columns: table => new
                {
                    CaracteristicaId = table.Column<Guid>(nullable: false),
                    DoteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoteCaracteristica", x => new { x.CaracteristicaId, x.DoteId });
                    table.ForeignKey(
                        name: "FK_DoteCaracteristica_Caracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoteCaracteristica_Dote_DoteId",
                        column: x => x.DoteId,
                        principalTable: "Dote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciaGenero",
                columns: table => new
                {
                    PreferenciaId = table.Column<Guid>(nullable: false),
                    Genero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciaGenero", x => new { x.PreferenciaId, x.Genero });
                    table.ForeignKey(
                        name: "FK_PreferenciaGenero_Preferencia_PreferenciaId",
                        column: x => x.PreferenciaId,
                        principalTable: "Preferencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoteCaracteristica_DoteId",
                table: "DoteCaracteristica",
                column: "DoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaracteristicaGenero");

            migrationBuilder.DropTable(
                name: "DoteCaracteristica");

            migrationBuilder.DropTable(
                name: "PreferenciaGenero");

            migrationBuilder.DropTable(
                name: "Caracteristica");

            migrationBuilder.DropTable(
                name: "Dote");

            migrationBuilder.DropTable(
                name: "Preferencia");

            migrationBuilder.DropColumn(
                name: "NotaAmbiente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "NotaEducacao",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "NotaPrazer",
                table: "Cliente",
                newName: "ValorPernoite");

            migrationBuilder.RenameColumn(
                name: "NotaPontualidade",
                table: "Cliente",
                newName: "Valor30min");

            migrationBuilder.RenameColumn(
                name: "NotaHigiene",
                table: "Cliente",
                newName: "Valor2horas");

            migrationBuilder.RenameColumn(
                name: "NotaFidelidadeAsFotos",
                table: "Cliente",
                newName: "Valor1Hora");

            migrationBuilder.AddColumn<bool>(
                name: "AtendeCasal",
                table: "Cliente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtendeHomem",
                table: "Cliente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtendeMulher",
                table: "Cliente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtendeTrans",
                table: "Cliente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PodeExibirEndereco",
                table: "Cliente",
                nullable: false,
                defaultValue: false);
        }
    }
}
