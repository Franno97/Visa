using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mre.Visas.Visa.Infrastructure.Migrations
{
    public partial class AddVisaElectronicaToVisa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisaElectronica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TramiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdSignatario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiasVigencia = table.Column<int>(type: "int", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroVisa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CalidadMigratoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroAdmisiones = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroPasaporte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoVerificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InformacionQR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreSignatario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombresBeneficiario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidosBeneficiario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DireccionDomiciliaria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActividadDesarrollar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequisitosCumplidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnidadAdministrativaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadAdministrativaNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaElectronica", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisaElectronica");
        }
    }
}
