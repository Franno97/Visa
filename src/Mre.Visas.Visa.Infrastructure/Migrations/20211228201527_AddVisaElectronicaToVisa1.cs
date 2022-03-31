using Microsoft.EntityFrameworkCore.Migrations;

namespace Mre.Visas.Visa.Infrastructure.Migrations
{
    public partial class AddVisaElectronicaToVisa1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecuenciaVisa",
                table: "VisaElectronica",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecuenciaVisa",
                table: "VisaElectronica");
        }
    }
}
