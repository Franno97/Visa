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
  public class ActualizarVisaElectronicaCommand : ActualizarVisaElectronicaRequest, IRequest<ApiResponseWrapper>
  {
    public ActualizarVisaElectronicaCommand(ActualizarVisaElectronicaRequest request)
    {
      Id = request.Id;
      Observaciones = request.Observaciones;
      SignatarioId = request.SignatarioId;
      DiasVigencia = request.DiasVigencia;
      NombreSignatario = request.NombreSignatario;
      UsuarioId = request.UsuarioId;
    }

    public class ActualizarVisaElectronicaCommandHandler : BaseHandler, IRequestHandler<ActualizarVisaElectronicaCommand, ApiResponseWrapper>
    {
      public ActualizarVisaElectronicaCommandHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      public async Task<ApiResponseWrapper> Handle(ActualizarVisaElectronicaCommand command, CancellationToken cancellationToken)
      {
        Domain.Entities.VisaElectronica visaElectronica = new();
        try
        {

          var visaElectronicas = UnitOfWork.VisaElectronicaRepository.GetById(command.Id);

          if (visaElectronicas == null || visaElectronicas.Result == null || visaElectronicas.Result.Count == 0)
            throw new Exception("Error al obtener visa electrónica");
          
          visaElectronica = visaElectronicas.Result.FirstOrDefault();

          visaElectronica.Observaciones= command.Observaciones;
          visaElectronica.SignatarioId = command.SignatarioId;
          visaElectronica.DiasVigencia = command.DiasVigencia;
          visaElectronica.NombreSignatario = command.NombreSignatario;
          visaElectronica.LastModifierId = command.UsuarioId;
          visaElectronica.LastModified = DateTime.Now;

          var resultado = UnitOfWork.VisaElectronicaRepository.Update(visaElectronica);
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
        response2.Mensaje = "Visa actualizada correctamente.";
        var response = new ApiResponseWrapper(HttpStatusCode.OK, response2);

        return response;
      }

    }

  }


  public class ActualizarVisaElectronicaCommandValidator : AbstractValidator<ActualizarVisaElectronicaCommand>
  {
    public ActualizarVisaElectronicaCommandValidator()
    {
      //RuleFor(e => e.Deadline)
      //    .NotEmpty().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} is required.")
      //    .NotNull().When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must not be null.")
      //    .GreaterThanOrEqualTo(e => e.StartDate).When(e => !string.IsNullOrEmpty(e.RecurrenceId)).WithMessage("{PropertyName} must be greater than or equal to the start date.");
    }
  }
}
