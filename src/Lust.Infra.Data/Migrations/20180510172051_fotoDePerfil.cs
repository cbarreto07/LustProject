using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lust.Infra.Data.Migrations
{
    public partial class fotoDePerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FotoDePerfil",
                table: "Cliente",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoDePerfil",
                table: "Cliente");
        }
    }
}
