﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Lust.Infra.CrossCutting.Identity.Migrations
{
    public partial class versionRev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "OpenIddictScopes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resources",
                table: "OpenIddictScopes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsentType",
                table: "OpenIddictApplications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "OpenIddictScopes");

            migrationBuilder.DropColumn(
                name: "Resources",
                table: "OpenIddictScopes");

            migrationBuilder.DropColumn(
                name: "ConsentType",
                table: "OpenIddictApplications");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
