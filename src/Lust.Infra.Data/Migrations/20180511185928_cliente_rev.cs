using Microsoft.EntityFrameworkCore.Migrations;

namespace Lust.Infra.Data.Migrations
{
    public partial class cliente_rev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorCobrado",
                table: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "LongaDescricao",
                table: "Cliente",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurtaDescricao",
                table: "Cliente",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LongaDescricao",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5000)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "CurtaDescricao",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorCobrado",
                table: "Cliente",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
