using Microsoft.EntityFrameworkCore.Migrations;

namespace Mre.Visas.Visa.Infrastructure.Migrations
{
    public partial class AddVisaElectronicaToVisa2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSignatario",
                table: "VisaElectronica",
                newName: "SignatarioId");

            migrationBuilder.AlterColumn<long>(
                name: "SecuenciaVisa",
                table: "VisaElectronica",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignatarioId",
                table: "VisaElectronica",
                newName: "IdSignatario");

            migrationBuilder.AlterColumn<int>(
                name: "SecuenciaVisa",
                table: "VisaElectronica",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
