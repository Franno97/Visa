using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mre.Visas.Visa.Infrastructure.Migrations
{
    public partial class AddVisaElectronicaToVisa4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UnidadAdministrativaId",
                table: "VisaElectronica",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UnidadAdministrativaId",
                table: "VisaElectronica",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
