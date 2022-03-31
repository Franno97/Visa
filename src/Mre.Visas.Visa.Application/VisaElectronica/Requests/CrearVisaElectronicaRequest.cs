using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Application.VisaElectronica.Requests
{
  public class CrearVisaElectronicaRequest
  {
    //public Guid Id{ get; set; }
    public Int64 NumeroInicial { get; set; }
    public Guid TramiteId { get; set; }
    public string Observaciones { get; set; }
    public Guid SignatarioId { get; set; }
    public string NombreSignatario { get; set; }
    public int DiasVigencia { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public Int64 SecuenciaVisa { get; set; }
    public string NumeroVisa { get; set; }
    public string CalidadMigratoria { get; set; }
    public string Categoria { get; set; }
    public string NumeroAdmisiones { get; set; }
    public string NumeroPasaporte { get; set; }
    public string CodigoVerificacion { get; set; }
    public string InformacionQR { get; set; }
    public string NombresBeneficiario { get; set; }
    public string ApellidosBeneficiario { get; set; }
    public string DireccionDomiciliaria { get; set; }
    public string ActividadDesarrollar { get; set; }
    public string RequisitosCumplidos { get; set; }
    public Guid UnidadAdministrativaId { get; set; }
    public string UnidadAdministrativaNombre { get; set; }
    public string UnidadAdministrativaCiudad { get; set; }
    public Guid UsuarioId { get; set; }
    public string FechaNacimiento { get; set; }
    public string Genero { get; set; }
    public string Nacionalidad { get; set; }
    public string FotoBeneficiario { get; set; }

  }

  public class ActualizarVisaElectronicaRequest
  {
    public Guid Id { get; set; }
    public string Observaciones { get; set; }
    public Guid SignatarioId { get; set; }
    public string NombreSignatario { get; set; }
    public int DiasVigencia { get; set; }
    public Guid UsuarioId { get; set; }
  }

  public class ObtenerCodigoBarrasRequest
  {
    /// <summary>
    /// Cadena de texto
    /// </summary>
    public string Cadena { get; set; }

    /// <summary>
    /// Tipo de Codigo [0 => Codigo de Barras; 1 => Codigo QR]
    /// </summary>
    public int TipoCodigo { get; set; }
  }
}
