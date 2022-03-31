using FluentValidation;
using MediatR;
using Mre.Visas.Visa.Application.Wrappers;
using Mre.Visas.Visa.Application.VisaElectronica.Requests;
using Mre.Visas.Visa.Application.Shared.Handlers;
using Mre.Visas.Visa.Application.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AutoMapper;
using Mre.Visas.Visa.Application.VisaElectronica.Responses;

namespace Mre.Visas.Visa.Application.VisaElectronica.Commands
{
  public class CrearVisaElectronicaCommand : CrearVisaElectronicaRequest, IRequest<ApiResponseWrapper>
  {
    public CrearVisaElectronicaCommand(CrearVisaElectronicaRequest request)
    {
      NumeroInicial = request.NumeroInicial;
      TramiteId = request.TramiteId;
      Observaciones = request.Observaciones;
      SignatarioId = request.SignatarioId;
      DiasVigencia = request.DiasVigencia;
      FechaEmision = request.FechaEmision;
      FechaExpiracion = request.FechaExpiracion;
      NumeroVisa = request.NumeroVisa;
      CalidadMigratoria = request.CalidadMigratoria;
      Categoria = request.Categoria;
      NumeroAdmisiones = request.NumeroAdmisiones;
      NumeroPasaporte = request.NumeroPasaporte;
      CodigoVerificacion = request.CodigoVerificacion;
      InformacionQR = request.InformacionQR;
      NombreSignatario = request.NombreSignatario;
      NombresBeneficiario = request.NombresBeneficiario;
      ApellidosBeneficiario = request.ApellidosBeneficiario;
      DireccionDomiciliaria = request.DireccionDomiciliaria;
      ActividadDesarrollar = request.ActividadDesarrollar;
      RequisitosCumplidos = request.RequisitosCumplidos;
      UnidadAdministrativaId = request.UnidadAdministrativaId;
      UnidadAdministrativaNombre = request.UnidadAdministrativaNombre;
      UnidadAdministrativaCiudad = request.UnidadAdministrativaCiudad;
      FechaNacimiento = request.FechaNacimiento;
      Genero = request.Genero;
      Nacionalidad = request.Nacionalidad;
      FotoBeneficiario = request.FotoBeneficiario;

      UsuarioId = request.UsuarioId;
    }

    public class CrearVisaElectronicaCommandHandler : BaseHandler, IRequestHandler<CrearVisaElectronicaCommand, ApiResponseWrapper>
    {
      public CrearVisaElectronicaCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      public async Task<ApiResponseWrapper> Handle(CrearVisaElectronicaCommand command, CancellationToken cancellationToken)
      {
        Domain.Entities.VisaElectronica visaElectronica = new Domain.Entities.VisaElectronica();

        // configurando el AutpMapper
        var config = new MapperConfiguration(cfg => cfg.CreateMap<CrearVisaElectronicaCommand, Domain.Entities.VisaElectronica>());
        var mapper = new Mapper(config);

        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Entities.VisaElectronica, CrearVisaElectronicaResponse>());
        var mapper2 = new Mapper(config2);

        try
        {

          var ultimaSecuencia = UnitOfWork.VisaElectronicaRepository.ObtenerSecuenciaVisaElectronica();

          //Si no tiene visas generadas, buscar el número inicial
          if (ultimaSecuencia == 0)
          {
            ultimaSecuencia = command.NumeroInicial;
          }

          ultimaSecuencia++;

          string salt = "xxxxxxxx";
          //Generar número de visa
          string numeroVisa = GenerarNumeroVisa(ultimaSecuencia, salt); //command.NumeroVisa
          //BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
          //codigo.IncludeLabel = true;
          //var imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, command.NumeroPasaporte);

          //Creando un nuevo objeto VisaElectronica

          visaElectronica = mapper.Map<Domain.Entities.VisaElectronica>(command);
          visaElectronica.AssignId();
          visaElectronica.NumeroVisa = numeroVisa;
          visaElectronica.SecuenciaVisa = ultimaSecuencia;
          visaElectronica.FechaEmision = DateTime.Now;
          visaElectronica.CodigoVerificacion = numeroVisa;
          visaElectronica.InformacionQR = numeroVisa;
          visaElectronica.FechaExpiracion = visaElectronica.FechaEmision.AddDays(visaElectronica.DiasVigencia);
          visaElectronica.SetearAuditable();

          var resultado = await UnitOfWork.VisaElectronicaRepository.InsertAsync(visaElectronica);
          if (!resultado.Item1)
            throw new Exception(resultado.Item2);

          resultado = await UnitOfWork.SaveChangesAsync();
          if (!resultado.Item1)
            throw new Exception(resultado.Item2);

        }
        catch (Exception ex)
        {
          return new ApiResponseWrapper(HttpStatusCode.BadRequest, new CrearVisaElectronicaResponse { Mensaje = ex.Message != null ? ex.Message : ex.InnerException.ToString(), Estado="Error" });
        }

        CrearVisaElectronicaResponse response2 = new CrearVisaElectronicaResponse();
        response2.Estado = "OK";
        response2.NumeroVisa = visaElectronica.NumeroVisa;
        response2.Mensaje = "Visa Generada correctamente.";
        var response = new ApiResponseWrapper(HttpStatusCode.OK, response2);

        return response;
      }

      private string GenerarNumeroVisa(Int64 ultimaSecuencia, string salt)
      {
        string password = ultimaSecuencia.ToString("000000000");

        Byte[] data = Encoding.ASCII.GetBytes(password);

        Byte[] sal = Encoding.ASCII.GetBytes(salt);

        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(data, sal, 2);

        string x = System.Convert.ToBase64String(rfc.GetBytes(6)).ToUpper();

        StringBuilder y = new StringBuilder();

        foreach (char item in x)
        {
          y.Append(char.IsLetterOrDigit(item) ? item : '7');
        }

        return y.ToString();

      }
    }

  }


  public class CrearVisaElectronicaCommandValidator : AbstractValidator<CrearVisaElectronicaCommand>
  {
    public CrearVisaElectronicaCommandValidator()
    {
      //RuleFor(e => e.NumeroVisa)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.NumeroVisa)).WithMessage("{PropertyName} es requerido.");
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}
