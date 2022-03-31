using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Mre.Visas.Visa.Infrastructure.Visa.Configurations
{
    public class VisaElectronicaConfiguration: IEntityTypeConfiguration<Domain.Entities.VisaElectronica>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.VisaElectronica> builder)
        {
            builder.ToTable("VisaElectronica");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.TramiteId).IsRequired(true);
            builder.Property(e => e.Observaciones).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.SignatarioId).IsRequired(true);
            builder.Property(e => e.NombreSignatario).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.DiasVigencia).IsRequired(true);
            builder.Property(e => e.FechaEmision).IsRequired(true);
            builder.Property(e => e.FechaExpiracion).IsRequired(true);
            builder.Property(e => e.SecuenciaVisa).IsRequired(true); 
            builder.Property(e => e.NumeroVisa).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.CalidadMigratoria).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.Categoria).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.NumeroAdmisiones).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.NumeroPasaporte).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.CodigoVerificacion).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.InformacionQR).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.NombresBeneficiario).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.ApellidosBeneficiario).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.DireccionDomiciliaria).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.ActividadDesarrollar).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.RequisitosCumplidos).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.UnidadAdministrativaId).IsRequired(true);
            builder.Property(e => e.UnidadAdministrativaNombre).IsRequired(true).HasMaxLength(50);
            builder.Property(e => e.UsuarioId).IsRequired(true);
            builder.Property(e => e.Created).IsRequired(true);
            builder.Property(e => e.CreatorId).IsRequired(true);
            builder.Property(e => e.LastModified).IsRequired(true);
            builder.Property(e => e.LastModifierId).IsRequired(true);

        }

    }
}

//add-migration AddVisaElectronicaToVisa -s Mre.Visas.Visa.Infrastructure
//update-database -s Mre.Visas.Visa.Infrastructure