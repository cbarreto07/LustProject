using Microsoft.EntityFrameworkCore.Migrations;

namespace Lust.Infra.Data.Migrations
{
    public partial class cliente_location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[Cliente] ADD [Location] geography");
            migrationBuilder.Sql(@"Create Trigger tgrCadastroCliente
                                    On Cliente 
                                    For Insert, Update
                                    as

                                    Begin

                                        Update A
                                           set A.Location = geography::Point(a.Latitude,a.Longitude, 4326)  
                                           From Cliente A
                                                Inner Join Inserted B On A.Id = B.Id
                                    End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
